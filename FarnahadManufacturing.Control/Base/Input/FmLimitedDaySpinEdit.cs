namespace FarnahadManufacturing.Control.Base.Input
{
    public class FmLimitedDaySpinEdit : FmIntSpinEdit
    {
        public FmLimitedDaySpinEdit()
        {
            MinValue = 1;
            MaxValue = 31;
        }
    }
}
