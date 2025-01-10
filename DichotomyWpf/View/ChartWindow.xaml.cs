using System.Collections.Generic;
using System.Windows;

namespace DichotomyWpf.View
{
    /// <summary>
    /// Interaction logic for ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window
    {
        public ChartWindow(Dictionary<double, double> points)
        {
            InitializeComponent();
            chart.Series[0].Points.DataBindXY(points.Keys, points.Values);
        }
    }
}
