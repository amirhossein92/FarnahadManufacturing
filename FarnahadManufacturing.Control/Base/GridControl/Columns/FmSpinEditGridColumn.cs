﻿using DevExpress.Xpf.Editors.Settings;

namespace FarnahadManufacturing.Control.Base.GridControl.Columns
{
    public class FmSpinEditGridColumn : FmGridColumn
    {
        public FmSpinEditGridColumn()
        {
            EditSettings = new SpinEditSettings();
        }
    }
}
