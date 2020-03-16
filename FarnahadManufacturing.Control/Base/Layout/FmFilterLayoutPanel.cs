using DevExpress.Xpf.Docking;

// CHECK
namespace FarnahadManufacturing.Control.Base.Layout
{
    public class FmFilterLayoutPanel : LayoutPanel
    {
        public FmFilterLayoutPanel()
        {
            Caption = "فیلتر";
            AllowDrag = false;
            AllowMove = false;
            AllowClose = false;
            MinWidth = 200;
        }
    }
}
