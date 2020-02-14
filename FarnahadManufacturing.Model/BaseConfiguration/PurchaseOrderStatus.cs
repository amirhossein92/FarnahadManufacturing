using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Base;

namespace FarnahadManufacturing.Model.BaseConfiguration
{
    /// <summary>
    /// وضعیت سفارش خرید
    /// </summary>
    public enum PurchaseOrderStatus
    {
        BidRequest = 10,
        ClosedShort = 20,
        Fulfilled = 30,
        Issued = 40,
        Partial = 50,
        Picked = 60,
        Picking = 70,
        Shipped = 80,
        Void = 90
    }
}
