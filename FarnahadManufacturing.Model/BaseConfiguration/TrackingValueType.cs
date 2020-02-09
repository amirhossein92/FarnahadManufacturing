﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.Model.BaseConfiguration
{
    /// <summary>
    /// نوع ردیابی
    /// </summary>
    public enum TrackingValueType
    {
        Text,
        Date,
        ExpirationDate,
        SerialNumber,
        Money,
        Quantity,
        Count,
        Checkbox
    }
}
