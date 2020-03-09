using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// CHECK
namespace FarnahadManufacturing.Control.Base.Input
{
    public class FmLimitedDaySpinEdit : FmIntSpinEdit
    {
        public FmLimitedDaySpinEdit()
        {
            MinValue = 1;
            MaxValue = 31;
        }
    }
}
