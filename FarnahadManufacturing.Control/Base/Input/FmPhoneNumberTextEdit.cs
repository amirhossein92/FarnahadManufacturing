using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Editors;

namespace FarnahadManufacturing.Control.Base.Input
{
    public class FmPhoneNumberTextEdit : FmTextEdit
    {
        public FmPhoneNumberTextEdit()
        {
            FlowDirection = FlowDirection.LeftToRight;
            MaskType = MaskType.RegEx;
            // TODO: Phone number regex
            //Mask = @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$";
            //Mask = @"^[+]*[0-9]{1,3}[\s/0-9]*$";
        }
    }
}
