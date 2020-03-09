using DevExpress.Xpf.LayoutControl;

// CHECK
namespace FarnahadManufacturing.Control.Base.Layout
{
    public class FmLayoutItem : LayoutItem
    {
        public FmLayoutItem()
        {
            this.AddColonToLabel = true;
            LabelWidth = 100;
        }
    }
}
