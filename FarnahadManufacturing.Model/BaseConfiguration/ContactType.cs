using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Model.BaseConfiguration
{
    /// <summary>
    /// نوع ارتباط
    /// </summary>
    public enum ContactType
    {
        Email = 10,
        Fax = 20,
        Home = 30,
        Main = 40,
        Mobile = 50,
        Other = 60,
        Pager = 70,
        Web = 80,
        Work = 90,
    }
}
