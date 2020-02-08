using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;

namespace FarnahadManufacturing.Model.Configuration
{
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
    }
}
