using DichotomyLib;
using DichotomyWpf.MVVM;

namespace DichotomyWpf.Model
{
    /// <summary>
    /// Використання патерну Адаптер для можливості data-binding'у з використанням точки
    /// </summary>
    public class NotifiedPoint : ViewModelBase
    {
        private Point point;

        public NotifiedPoint(double x, double y)
        {
            point = new Point(x, y);
        }

        public double X
        {
            get { return point.X; }
            set
            {
                point.X = value;
                NotifyPropertyChanged();
            }
        }

        public double Y
        {
            get { return point.Y; }
            set
            {
                point.Y = value;
                NotifyPropertyChanged();
            }
        }
    }
}
