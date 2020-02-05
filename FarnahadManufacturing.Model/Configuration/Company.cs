﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;

namespace FarnahadManufacturing.Model.Configuration
{
    public class Company : FmModelBase
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

        private byte[] _logo;
        public byte[] Logo
        {
            get => _logo;
            set
            {
                _logo = value;
                OnPropertyChanged();
            }
        }

        private int _defaultCarrierId;
        public int DefaultCarrierId
        {
            get => _defaultCarrierId;
            set
            {
                _defaultCarrierId = value;
                OnPropertyChanged();
            }
        }

        private Carrier _defaultCarrier;
        public Carrier DefaultCarrier
        {
            get => _defaultCarrier;
            set
            {
                _defaultCarrier = value;
                OnPropertyChanged();
            }
        }

        private List<Address> _address;
        public List<Address> Addresses
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }
    }
}