namespace FarnahadManufacturing.Control.Base.ViewModel
{
    public class FmComboModel<T>
    {
        public FmComboModel()
        {

        }

        public FmComboModel(T value, string title) : this()
        {
            Value = value;
            Title = title;
        }

        public T Value { get; set; }
        public string Title { get; set; }
    }
}
