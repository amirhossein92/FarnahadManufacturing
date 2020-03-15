namespace FarnahadManufacturing.Test.Base
{
    public abstract class ViewModelBase : BindableBase
    {

        //public Action<Type> NavigationEvent { get; set; }

        //public void Navigation<T>() where T : ViewModelBase
        //{
        //    NavigationEvent?.Invoke(typeof(T));
        //}
        public abstract void LoadData();

        public string _title;
        public string Title
        {
            get => _title;
            set => SetValue(ref _title, value);
        }
    }
}
