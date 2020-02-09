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
        BidRequest,
        ClosedShort,
        Fulfilled,
        Issued,
        Partial,
        Picked,
        Picking,
        Shipped,
        Void
    }
}
