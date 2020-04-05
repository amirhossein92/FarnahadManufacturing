using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace FarnahadManufacturing.Mvvm.Base
{
    public class UserControlBase : UserControl
    {
        public UserControlBase()
        {
            BindLoadDataEvent();
        }

        private void BindLoadDataEvent()
        {
            var ev = new Microsoft.Xaml.Behaviors.EventTrigger() { EventName = "Loaded" };
            ev.SourceObject = this;
            var callMethod = new CallMethodAction { MethodName = "LoadData" };
            var binding = new Binding();
            BindingOperations.SetBinding(callMethod, CallMethodAction.TargetObjectProperty, binding);
            ev.Actions.Add(callMethod);

            Interaction.GetTriggers(this).Add(ev);
        }
    }
}
