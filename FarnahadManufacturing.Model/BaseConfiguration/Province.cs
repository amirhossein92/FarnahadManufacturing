﻿using System;
using System.Collections.Generic;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Model.BaseConfiguration
{
    public class Province : FmModelBase
    {
        /// <summary>
        /// استان
        /// </summary>
        public Province()
        {
            Cities = new List<City>();
            Addresses = new List<Address>();
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
        /// کشور
        /// </summary>
        private int _countryId;
        public int CountryId
        {
            get => _countryId;
            set
            {
                _countryId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// کشور
        /// </summary>
        private Country _country;
        public Country Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }

        private List<City> _cities;
        public List<City> Cities
        {
            get => _cities;
            set
            {
                _cities = value;
                OnPropertyChanged();
            }
        }

        private List<Address> _addresses;
        public List<Address> Addresses
        {
            get => _addresses;
            set
            {
                _addresses = value;
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
