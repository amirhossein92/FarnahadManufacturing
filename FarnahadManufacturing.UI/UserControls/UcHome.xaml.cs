﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FarnahadManufacturing.Control.Base.UserControl;

namespace FarnahadManufacturing.UI.UserControls
{
    public partial class UcHome : UserControlPage
    {
        public UcHome()
        {
            InitializeComponent();

            UserControlTitle = "خانه";
            ImagePath = "Icons/NavigationBar/Home_Small.svg";
        }

        protected override void SetToolBarItems()
        {
        }

        protected override void InitialData()
        {
        }
    }
}
