using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Editors;

// CHECK
namespace FarnahadManufacturing.Control.Base.Input
{
    public class FmEmailTextEdit : FmTextEdit
    {
        public FmEmailTextEdit()
        {
            MaskType = MaskType.RegEx;
            Mask = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}";
            MaskUseAsDisplayFormat = true;
        }
    }
}
