using System.ComponentModel;
using System.Runtime.CompilerServices;

// CHECK
namespace FarnahadManufacturing.Base
{
    public class FmModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
