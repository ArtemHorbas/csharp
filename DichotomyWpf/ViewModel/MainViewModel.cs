using DichotomyLib;
using DichotomyLib.exeption;
using DichotomyWpf.Model;
using DichotomyWpf.MVVM;
using DichotomyWpf.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Windows = System.Windows;

namespace DichotomyWpf.ViewModel
{
    public class MainViewModel : ViewModelBase, ICloseable
    {
        private ReportableDichotomy<MFunction> dichotomy;
        private Dictionary<double, double> functionPairs;

        public BindingList<NotifiedPoint> Points { get; set; }
        public BindingList<NotifiedCoef> Coefs { get; set; }

        public IList<int> PointsCount { get; set; }
        public IList<int> CoefsCount { get; set; }

        private int selectedPointIndex;
        private int selectedCoefIndex;

        private double minimumValue;
        private string visibleMinimum;

        private string startValue;
        private string endValue;
        private string accuracyValue;

        private event Action ChangedSelectedPointIndex;
        private event Action ChangedSelectedCoefIndex;
        private event Action SuccesfullyDeserialized;
        public event Action ClosedRequest;

        public ICommand NewCommand => new RelayCommand(execute => NewData());
        public ICommand SerializeCommand => new RelayCommand(execute => Serialize());
        public ICommand DeserializeCommand => new RelayCommand(execute => Deserialize());
        public ICommand ExitCommand => new RelayCommand(execute => ClosedRequest?.Invoke());

        public ICommand ExecuteCommand => new RelayCommand(execute => Execute());
        public ICommand ReportCommand => new RelayCommand(execute => GenerateReport());
        public ICommand DrawCommand => new RelayCommand(execute => DrawChart());

        public MainViewModel()
        {
            dichotomy = new ReportableDichotomy<MFunction>();
            Points = new BindingList<NotifiedPoint>();
            Coefs = new BindingList<NotifiedCoef>();
            functionPairs = new Dictionary<double, double>();

            Points.ListChanged += OnPointChanged;
            Coefs.ListChanged += OnCoefChanged;

            int[] count = new int[] { 1, 2, 3, 4, 5, 6 };
            PointsCount = count;
            CoefsCount = count;

            ChangedSelectedPointIndex += InitPoints;
            ChangedSelectedCoefIndex += InitCoefs;
            SelectedPointIndex = 1;
            SelectedCoefIndex = 1;

            SuccesfullyDeserialized += MergePoints;
            SuccesfullyDeserialized += MergeCoefs;
            SuccesfullyDeserialized += MergeSelectedPointIndex;
            SuccesfullyDeserialized += MergeSelectedCoefIndex;

            visibleMinimum = "Hidden";
            startValue = "-1";
            endValue = "3";
            accuracyValue = "0.01";
        }

        //Реактивні властивості
        public int SelectedPointIndex
        {
            get { return selectedPointIndex; }
            set 
            { 
                selectedPointIndex = value;
                NotifyPropertyChanged();
                ChangedSelectedPointIndex?.Invoke();
            }
        }

        public int SelectedCoefIndex
        {
            get { return selectedCoefIndex; }
            set 
            { 
                selectedCoefIndex = value;
                NotifyPropertyChanged();
                ChangedSelectedCoefIndex?.Invoke();
            }
        }

        public double MinimumValue
        {
            get { return minimumValue; }
            set
            {
                minimumValue = value;
                NotifyPropertyChanged();
            }
        }

        public string VisibleMinimum
        {
            get { return visibleMinimum; }
            set
            {
                visibleMinimum = value;
                NotifyPropertyChanged();
            }
        }

        public string StartValue
        {
            get { return startValue; }
            set
            {
                startValue = value;
                NotifyPropertyChanged();
            }
        }

        public string EndValue
        {
            get { return endValue; }
            set
            {
                endValue = value;
                NotifyPropertyChanged();
            }
        }

        public string AccuracyValue
        {
            get { return accuracyValue; }
            set
            {
                accuracyValue = value;
                NotifyPropertyChanged();
            }
        }

        private void InitPoints()
        {
            Points.Clear();
            for(int i = 0; i <= selectedPointIndex; i++)
            {
                Points.Add(new NotifiedPoint(i, i));
            }
        }

        private void InitCoefs()
        {
            Coefs.Clear();
            for (int i = 0; i <= selectedCoefIndex; i++)
            {
                Coefs.Add(new NotifiedCoef(i, i+1));
            }
        }

        private void OnPointChanged(object sender, ListChangedEventArgs e)
        {
            dichotomy.Function.GFunction.Points.Clear();
            foreach (NotifiedPoint p in Points)
            {
                dichotomy.Function.GFunction.AddPoint(p.X, p.Y);
            }
        }

        private void OnCoefChanged(object sender, ListChangedEventArgs e)
        {
            dichotomy.Function.FFunction.Coefs.Clear();
            foreach (NotifiedCoef c in Coefs)
            {
                dichotomy.Function.FFunction.AddCoef(c.Index, c.Value);
            }
        }

        private void NewData()
        {
            SelectedPointIndex = 1;
            SelectedCoefIndex = 1;
            MinimumValue = 0;
            VisibleMinimum = "Hidden";
            StartValue = "-1";
            EndValue = "3";
            AccuracyValue = "0.01";
        }

        private void Serialize()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "XML file | *.xml";
            fileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            bool? result = fileDialog.ShowDialog();
            if(result == true)
            {
                try
                {
                    string fileName = fileDialog.SafeFileName;
                    dichotomy.Serialize(fileName);
                }
                catch (IOException)
                {
                    Windows.MessageBox.Show("Error while writing, check your file",
                    "IOException", Windows.MessageBoxButton.OK, Windows.MessageBoxImage.Error);
                }
                catch (Exception)
                {
                    Windows.MessageBox.Show("An error occured",
                    "Exception", Windows.MessageBoxButton.OK, Windows.MessageBoxImage.Error);
                }
            }
        }

        private void Deserialize()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "XML file | *.xml";
            fileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                try
                {
                    string fileName = fileDialog.SafeFileName;
                    dichotomy.Deserialize(fileName);
                    SuccesfullyDeserialized?.Invoke();
                }
                catch (IOException)
                {
                    Windows.MessageBox.Show("Error while reading, check your file",
                    "IOException", Windows.MessageBoxButton.OK, Windows.MessageBoxImage.Error);
                }
                catch (Exception)
                {
                    Windows.MessageBox.Show("An error occured",
                    "Exception", Windows.MessageBoxButton.OK, Windows.MessageBoxImage.Error);
                }
            }
        }

        private void Execute()
        {
            try 
            {
                double start = double.Parse(StartValue);
                double end = double.Parse(EndValue);
                double accuracy = double.Parse(AccuracyValue);
                VisibleMinimum = "Visible";
                MinimumValue = double.Parse($"{(dichotomy.GetMinimum(start, end, accuracy)):f8}");
            }
            catch (IncorrectRangeException ex)
            {
                Windows.MessageBox.Show($"Incorrect range was provided: [{ex.Start}, {ex.End}]. Provide range " +
                    $"with only one minimum value", 
                    "Range", Windows.MessageBoxButton.OK, Windows.MessageBoxImage.Error);
                VisibleMinimum = "Hidden";
            }
        }

        private void GenerateReport()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "HTML file | *.html";
            fileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                string fileName = fileDialog.SafeFileName;
                try
                {
                    double start = double.Parse(StartValue);
                    double end = double.Parse(EndValue);
                    double accuracy = double.Parse(AccuracyValue);
                    dichotomy.GenerateReport(fileName, start, end, accuracy);
                }
                catch (FormatException)
                {
                    Windows.MessageBox.Show("Wrong parametr type. Write a double value", 
                        "Parametrs", Windows.MessageBoxButton.OK, Windows.MessageBoxImage.Error);
                }
                catch (IOException)
                {
                    Windows.MessageBox.Show("Error while writing, check your file",
                    "IOException", Windows.MessageBoxButton.OK, Windows.MessageBoxImage.Error);
                }
                catch (Exception)
                {
                    Windows.MessageBox.Show("An error occured",
                    "Exception", Windows.MessageBoxButton.OK, Windows.MessageBoxImage.Error);
                }
            }
        }

        private void DrawChart() 
        {
            InitFunctionPairs();
            ChartWindow chartWindow = new ChartWindow(functionPairs);
            chartWindow.ShowDialog();
        }

        private void InitFunctionPairs()
        {
            functionPairs.Clear();
            try
            {
                double start = double.Parse(StartValue);
                double end = double.Parse(EndValue);
                double accuracy = double.Parse(AccuracyValue);
                for (double i = start; i <= end; i+=accuracy)
                {
                    functionPairs.Add(i, dichotomy.Function.GetValue(i));
                }
            }
            catch (FormatException)
            {
                Windows.MessageBox.Show("Wrong parametr type. Write a double value",
                    "Parametrs", Windows.MessageBoxButton.OK, Windows.MessageBoxImage.Error);
            }
            catch (ArgumentException)
            {
                Windows.MessageBox.Show("Function must has only one minimum value. Change range",
                    "Parametrs", Windows.MessageBoxButton.OK, Windows.MessageBoxImage.Error);
            }
        }

        private void MergePoints()
        {
            Points.ListChanged -= OnPointChanged;
            Points.Clear();
            foreach (Point p in dichotomy.Function.GFunction.Points)
            {
                Points.Add(new NotifiedPoint(p.X, p.Y));
            }
            Points.ListChanged += OnPointChanged;
        }

        private void MergeCoefs()
        {
            Coefs.ListChanged -= OnCoefChanged;
            Coefs.Clear();
            foreach (Coef c in dichotomy.Function.FFunction.Coefs)
            {
                Coefs.Add(new NotifiedCoef(c.Index, c.Value));
            }
            Coefs.ListChanged += OnCoefChanged;
        }

        private void MergeSelectedPointIndex()
        {
            ChangedSelectedPointIndex -= InitPoints;
            SelectedPointIndex = Points.Count - 1;
            ChangedSelectedPointIndex += InitPoints;
        }

        private void MergeSelectedCoefIndex()
        {
            ChangedSelectedCoefIndex -= InitCoefs;
            SelectedCoefIndex = Coefs.Count - 1;
            ChangedSelectedCoefIndex += InitCoefs;
        }
    }
}
