using FarnahadManufacturing.Mvvm.Base;
using FarnahadManufacturing.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FarnahadManufacturing.Mvvm.Utility.Extensions
{
    public static class MyViewLocator
    {
        public static KeyValuePair<object, object> BindViewModelToView<TViewModel, TView>() where TViewModel : ViewModelBase where TView : UserControlBase
        {
            DataTemplate dataTemplate = new DataTemplate
            {
                VisualTree = new FrameworkElementFactory(typeof(TView))
            };
            var dtk = new DataTemplateKey(typeof(TViewModel));
            return new KeyValuePair<object, object>(dtk, dataTemplate);
        }
    }
}
