

// CHECK
namespace FarnahadManufacturing.Control.Base.Input
{
    public class FmDaySpinEdit : FmIntSpinEdit
    {
        public FmDaySpinEdit()
        {
            MinValue = 1;
            Mask = "#### روز";
        }
    }
}
