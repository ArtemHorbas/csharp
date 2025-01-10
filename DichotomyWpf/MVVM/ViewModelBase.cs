using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DichotomyWpf.MVVM
{
    /// <summary>
    /// Клас реалізує інтерфейс INotifyPropertyChanged.
    /// Представлюячи метод для автоматично повідомлення підписників про зміну значення
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
