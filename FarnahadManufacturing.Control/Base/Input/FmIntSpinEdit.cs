﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// CHECK
namespace FarnahadManufacturing.Control.Base.Input
{
    public class FmIntSpinEdit : FmSpinEdit
    {
        public FmIntSpinEdit()
        {
            Mask = "D0";
            EditValueType = typeof(int);
        }
    }
}
