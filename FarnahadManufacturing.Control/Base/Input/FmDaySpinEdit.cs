using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.Control.Base.Input
{
    public class FmDaySpinEdit : FmIntSpinEdit
    {
        public FmDaySpinEdit()
        {
            MinValue = 1;
            Mask = "#### روز";
        }
    }
}
