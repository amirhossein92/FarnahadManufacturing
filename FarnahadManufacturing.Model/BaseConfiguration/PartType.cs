using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;

namespace FarnahadManufacturing.Model.BaseConfiguration
{
    /// <summary>
    /// نوع کالا
    /// </summary>
    public enum PartType
    {
        CapitalEquipment = 10,
        InternalUse = 20,
        Inventory = 30,
        Labor = 40,
        Misc = 50,
        NonInventory = 60,
        Overhead = 70,
        Service = 80,
        Shipping = 90,
        Tax = 100,
    }
}
