using System;
using FarnahadManufacturing.Base;

// CHECK
namespace FarnahadManufacturing.Model.Configuration
{
    /// <summary>
    /// قیمت محصولات
    /// نباید قابل ویرایش باشه این جدول
    /// </summary>
    public class ProductPrice : FmModelBase
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// محصول
        /// </summary>
        private int _productId;
        public int ProductId
        {
            get => _productId;
            set
            {
                _productId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// محصول
        /// </summary>
        private Product _product;
        public Product Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// قیمت
        /// </summary>
        private double _price;
        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private int _createdByUserId;
        public int CreatedByUserId
        {
            get => _createdByUserId;
            set
            {
                _createdByUserId = value;
                OnPropertyChanged();
            }
        }

        private User _createdByUser;
        public User CreatedByUser
        {
            get => _createdByUser;
            set
            {
                _createdByUser = value;
                OnPropertyChanged();
            }
        }

        private DateTime _createdDateTime;
        public DateTime CreatedDateTime
        {
            get => _createdDateTime;
            set
            {
                _createdDateTime = value;
                OnPropertyChanged();
            }
        }
    }
}
