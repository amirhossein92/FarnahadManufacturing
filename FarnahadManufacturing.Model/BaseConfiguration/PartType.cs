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
        Inventory,
        Service,
        Labor,
        Overhead,
        NonInventory,
        InternalUse,
        CapitalEquipment,
        Shipping,
        Tax,
        Misc
    }
}
