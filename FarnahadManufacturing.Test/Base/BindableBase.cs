using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
