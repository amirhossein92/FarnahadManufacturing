using DevExpress.Xpf.Bars;
using FarnahadManufacturing.Control.Common;

// CHECK
namespace FarnahadManufacturing.Control.Base.ToolBar.Buttons
{
    public class FmSaveBarButtonItem : BarButtonItem
    {
        public FmSaveBarButtonItem()
        {
            Name = "Save";
            Content = "ذخیره";
            Glyph = ImageUtility.CreateSvgImage("Icons/ToolBar/Save.svg");
        }
    }
}
