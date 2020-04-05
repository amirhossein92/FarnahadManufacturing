﻿using FarnahadManufacturing.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmTest.ViewModels
{
    public class TestUserControlViewModel : ViewModelBase
    {
        public TestUserControlViewModel()
        {

        }

        public override void LoadCommands()
        {
        }

        public async override void LoadData()
        {
            await Task.Delay(3000);
            Test = "Test String Loaded Asyncronous after 3 seconds from ViewModel";
        }

        public override string SetTitle()
        {
            return "Test UC";
        }

        private string _test;
        public string Test
        {
            get => _test;
            set
            {
                _test = value;
                OnPropertyChanged();
            }
        }
    }
}
