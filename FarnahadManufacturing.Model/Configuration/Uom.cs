﻿using System;
using System.Collections.Generic;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Model.Configuration
{
    /// <summary>
    /// واحد اندازه گیری
    /// </summary>
    public class Uom : FmModelBase
    {
        public Uom()
        {
            Parts = new List<Part>();
            PartsDistanceUom = new List<Part>();
            PartsWeightUom = new List<Part>();
            Products = new List<Product>();
            ProductsDistanceUom = new List<Product>();
            ProductsWeightUom = new List<Product>();
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
        /// مخفف
        /// </summary>
        private string _abbreviation;
        public string Abbreviation
        {
            get => _abbreviation;
            set
            {
                _abbreviation = value;
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
        /// نرخ تبدیل
        /// </summary>
        private double _conversion;
        public double Conversion
        {
            get => _conversion;
            set
            {
                _conversion = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// نوع واحد اندازه گیری
        /// </summary>
        private UomType _uomType;
        public UomType UomType
        {
            get => _uomType;
            set
            {
                _uomType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// غیر قابل تغییر
        /// </summary>
        private bool _readOnly;
        public bool ReadOnly
        {
            get => _readOnly;
            set
            {
                _readOnly = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// فعال
        /// </summary>
        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                OnPropertyChanged();
            }
        }

        private List<Part> _parts;
        public List<Part> Parts
        {
            get => _parts;
            set
            {
                _parts = value;
                OnPropertyChanged();
            }
        }

        private List<Part> _partsDistanceUom;
        public List<Part> PartsDistanceUom
        {
            get => _partsDistanceUom;
            set
            {
                _partsDistanceUom = value;
                OnPropertyChanged();
            }
        }

        private List<Part> _partsWeightUom;
        public List<Part> PartsWeightUom
        {
            get => _partsWeightUom;
            set
            {
                _partsWeightUom = value;
                OnPropertyChanged();
            }
        }

        private List<Product> _products;
        public List<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        private List<Product> _productsDistanceUom;
        public List<Product> ProductsDistanceUom
        {
            get => _productsDistanceUom;
            set
            {
                _productsDistanceUom = value;
                OnPropertyChanged();
            }
        }

        private List<Product> _productsWeightUom;
        public List<Product> ProductsWeightUom
        {
            get => _productsWeightUom;
            set
            {
                _productsWeightUom = value;
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
