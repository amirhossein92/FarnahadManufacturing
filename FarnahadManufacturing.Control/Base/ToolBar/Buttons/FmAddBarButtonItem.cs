using DevExpress.Xpf.Bars;
using FarnahadManufacturing.Control.Common;

namespace FarnahadManufacturing.Control.Base.ToolBar.Buttons
{
    public class FmAddBarButtonItem : BarButtonItem
    {
        public FmAddBarButtonItem()
        {
            Name = "Add";
            Content = "اضافه";
            Glyph = ImageUtility.CreateSvgImage("Icons/ToolBar/Add.svg");
        }
    }
}
