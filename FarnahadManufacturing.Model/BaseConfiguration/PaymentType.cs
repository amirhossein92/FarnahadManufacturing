using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.Model.BaseConfiguration
{
    /// <summary>
    /// نوع پرداخت
    /// </summary>
    public enum PaymentType
    {
        Cash = 10,
        Check = 30,
        CreditCard = 30,
        ECheck = 40,
        GiftCard = 50,
        Other = 60
    }
}
