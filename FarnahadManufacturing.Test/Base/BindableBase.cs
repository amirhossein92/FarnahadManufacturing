using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FarnahadManufacturing.Test.Base
{
    public class BindableBase : INotifyPropertyChanged
    {
        public void SetValue<T>(ref T value, T newValue, [CallerMemberName] string memberName = null)
        {
            if (!newValue.Equals(value))
            {
                value = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
