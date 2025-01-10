using DichotomyWpf.ViewModel;
using System.Windows;

namespace DichotomyWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MainViewModel vm = new MainViewModel();
            DataContext = vm;
            if (DataContext is ICloseable c)
            {
                c.ClosedRequest += () => Close();
            }
            InitializeComponent();
        }
    }
}
