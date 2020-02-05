﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;

namespace FarnahadManufacturing.Model.Configuration
{
    public class ProductCategory : FmModelBase
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

        private int? _parentProductCategoryId;
        public int? ParentProductCategoryId
        {
            get => _parentProductCategoryId;
            set
            {
                _parentProductCategoryId = value;
                OnPropertyChanged();
            }
        }

        private ProductCategory _parentProductCategory;
        public ProductCategory ParentProductCategory
        {
            get => _parentProductCategory;
            set
            {
                _parentProductCategory = value;
                OnPropertyChanged();
            }
        }
    }
}