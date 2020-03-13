// CHECK
namespace FarnahadManufacturing.Model.Configuration
{
    public class MyCompany : Company
    {
        /// <summary>
        /// معاف از مالیات
        /// </summary>
        private bool _isTaxExempt;
        public bool IsTaxExempt
        {
            get => _isTaxExempt;
            set
            {
                _isTaxExempt = value;
                OnPropertyChanged();
            }
        }
    }
}
