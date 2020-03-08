using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<FarnahadManufacturing.Data.FarnahadManufacturingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FarnahadManufacturing.Data.FarnahadManufacturingDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Users.AddOrUpdate(x => x.Id,
                new User
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = "Admin",
                    PasswordSalt = Guid.NewGuid().ToString(),
                    Password = "admin",
                    PhoneNumber = "+123",
                    Email = "admin@admin.ir",
                    CreatedDateTime = DateTime.Now,
                    IsActive = true
                });

            context.MyCompanies.AddOrUpdate(x => x.Id,
                new MyCompany
                {
                    Id = 1,
                    Title = "My Company",
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                });

            context.PaymentTerms.AddOrUpdate(x => x.Id,
                new PaymentTerm
                {
                    Id = 1,
                    Title = "پرداخت در زمان تحویل/سفارش",
                    Description = "CON (Cash on Delivery)",
                    ReadOnly = true,
                    IsActive = true,
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                },
                new PaymentTerm
                {
                    Id = 2,
                    Title = "پرداخت قبل از تحویل/سفارش",
                    Description = "CAD (Cash in Advance)",
                    ReadOnly = true,
                    IsActive = true,
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                });

            context.AddressTypes.AddOrUpdate(x => x.Id,
                new AddressType
                {
                    Id = 1,
                    Title = "تحویل صورتحساب",
                    Description = "Bill To",
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                },
                new AddressType
                {
                    Id = 2,
                    Title = "تحویل مرسولات",
                    Description = "Ship To",
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                },
                new AddressType
                {
                    Id = 3,
                    Title = "دفتر مرکزی",
                    Description = "Main Office",
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                },
                new AddressType
                {
                    Id = 4,
                    Title = "تحویل رسید پرداخت",
                    Description = "Remit To",
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                },
                new AddressType
                {
                    Id = 5,
                    Title = "خانه",
                    Description = "Home",
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                });

            context.FobTypes.AddOrUpdate(x => x.Id,
                new FobType
                {
                    Id = 1,
                    Title = "نامعلوم",
                    Description = "",
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                },
                new FobType
                {
                    Id = 2,
                    Title = "مقصد",
                    Description = "",
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                },
                new FobType
                {
                    Id = 3,
                    Title = "مبدا",
                    Description = "",
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                },
                new FobType
                {
                    Id = 4,
                    Title = "بندر ورودی",
                    Description = "",
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                });

            context.ShippingTerms.AddOrUpdate(x => x.Id,
            new ShippingTerm
            {
                Id = 1,
                Title = "Prepaid and Billed",
                CreatedByUserId = 1,
                CreatedDateTime = DateTime.Now
            },
            new ShippingTerm
            {
                Id = 2,
                Title = "Prepaid",
                CreatedByUserId = 1,
                CreatedDateTime = DateTime.Now
            },
            new ShippingTerm
            {
                Id = 3,
                Title = "Freight Collect",
                CreatedByUserId = 1,
                CreatedDateTime = DateTime.Now
            });

            context.Trackings.AddOrUpdate(x => x.Id,
                new Tracking
                {
                    Id = 1,
                    Title = "تاریخ انقضا",
                    Abbreviation = "EXP",
                    TrackingValueType = TrackingValueType.ExpirationDate,
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                },
                new Tracking
                {
                    Id = 2,
                    Title = "شماره سریال",
                    Abbreviation = "SN",
                    TrackingValueType = TrackingValueType.SerialNumber,
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                },
                new Tracking
                {
                    Id = 3,
                    Title = "رنگ",
                    Abbreviation = "CLR",
                    TrackingValueType = TrackingValueType.Text,
                    CreatedByUserId = 1,
                    CreatedDateTime = DateTime.Now,
                });
        }
    }
}
