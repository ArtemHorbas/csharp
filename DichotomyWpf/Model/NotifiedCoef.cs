using DichotomyLib;
using DichotomyWpf.MVVM;

namespace DichotomyWpf.Model
{
    /// <summary>
    /// Використання патерну Адаптер для можливості data-binding'у з використанням коефіцієнта
    /// </summary>
    public class NotifiedCoef : ViewModelBase
    {
        private Coef coef;

        public NotifiedCoef(int index, double value)
        {
            coef = new Coef(index, value);
        }

        public int Index
        {
            get { return coef.Index; }
            set
            {
                coef.Index = value;
                NotifyPropertyChanged();
            }
        }

        public double Value
        {
            get { return coef.Value; }
            set
            {
                coef.Value = value;
                NotifyPropertyChanged();
            }
        }
    }
}
