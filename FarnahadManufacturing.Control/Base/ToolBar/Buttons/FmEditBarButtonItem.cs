using DevExpress.Xpf.Bars;
using FarnahadManufacturing.Control.Common;

// CHECK
namespace FarnahadManufacturing.Control.Base.ToolBar.Buttons
{
    public class FmEditBarButtonItem : BarButtonItem
    {
        public FmEditBarButtonItem()
        {
            Name = "Edit";
            Content = "ویرایش";
            Glyph = ImageUtility.CreateSvgImage("Icons/ToolBar/Edit.svg");
        }
    }
}
