﻿using DevExpress.Xpf.Editors.Settings;

namespace FarnahadManufacturing.Control.Base.GridControl.Columns
{
    public class FmPercentageSpinEditGridColumn : FmSpinEditGridColumn
    {
        public FmPercentageSpinEditGridColumn()
        {
            EditSettings = new SpinEditSettings { Mask = "p" };
        }
    }
}
