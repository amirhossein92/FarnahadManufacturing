using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;

namespace FarnahadManufacturing.Model.Configuration
{
    // TODO: New Model
    /// <summary>
    /// محصول جایگزین
    /// </summary>
    public class ProductSubstitute : FmModelBase
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
        /// محصول جایگزین
        /// </summary>
        private int _substituteProductId;
        public int SubstituteProductId
        {
            get => _substituteProductId;
            set
            {
                _substituteProductId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// محصول جایگزین
        /// </summary>
        private Product _substituteProduct;
        public Product SubstituteProduct
        {
            get => _substituteProduct;
            set
            {
                _substituteProduct = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// یادداشت
        /// </summary>
        private string _note;
        public string Note
        {
            get => _note;
            set
            {
                _note = value;
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
