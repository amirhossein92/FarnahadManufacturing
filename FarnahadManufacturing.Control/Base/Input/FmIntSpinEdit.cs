

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
