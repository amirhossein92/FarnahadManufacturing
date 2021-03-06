﻿namespace FarnahadManufacturing.Model.BaseConfiguration
{
    /// <summary>
    /// نوع محل مثلا انبار، فروشگاه و غیره
    /// </summary>
    public enum LocationType
    {
        Consignment = 10,
        Inspection = 20,
        Locked = 30,
        Manufacturing = 40,
        Picking = 50,
        Receiving = 60,
        Shipping = 70,
        Stock = 80,
        StoreFront = 90,
        Vendor = 100,
    }
}
