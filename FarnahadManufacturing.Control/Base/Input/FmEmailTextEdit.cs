using System.Windows;
using DevExpress.Xpf.Editors;

namespace FarnahadManufacturing.Control.Base.Input
{
    public class FmEmailTextEdit : FmTextEdit
    {
        public FmEmailTextEdit()
        {
            MaskType = MaskType.RegEx;
            Mask = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}";
            MaskUseAsDisplayFormat = true;
            FlowDirection = FlowDirection.LeftToRight;
        }
    }
}
