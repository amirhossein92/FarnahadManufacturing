using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Editors;

namespace FarnahadManufacturing.UI.Base.Input
{
    public class FmSpinEdit : SpinEdit
    {
        public FmSpinEdit()
        {
            FlowDirection = FlowDirection.LeftToRight;
        }
    }
}
