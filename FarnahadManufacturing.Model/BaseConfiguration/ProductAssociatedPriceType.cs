using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Model.BaseConfiguration
{
    /// <summary>
    /// نوع هزینه مرتبط با محصول
    /// </summary>
    /// TODO: NEW MODEL
    public class ProductAssociatedPriceType : FmModelBase
    {
        public ProductAssociatedPriceType()
        {
            ProductAssociatePrices = new List<ProductAssociatePrice>();
        }

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
        /// عنوان
        /// </summary>
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// توضیحات
        /// </summary>
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// مشمول مالیات
        /// </summary>
        private bool _isTaxable;
        public bool IsTaxable
        {
            get => _isTaxable;
            set
            {
                _isTaxable = value;
                OnPropertyChanged();
            }
        }

        private List<ProductAssociatePrice> _productAssociatePrices;
        public List<ProductAssociatePrice> ProductAssociatePrices
        {
            get => _productAssociatePrices;
            set
            {
                _productAssociatePrices = value;
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
