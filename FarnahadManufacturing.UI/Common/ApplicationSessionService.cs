using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Data;

namespace FarnahadManufacturing.UI.Common
{
    public static class ApplicationSessionService
    {
        public static DateTime GetNowDateTime()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var query = dbContext.Database.SqlQuery<DateTime>("SELECT getdate()");
                return query.AsEnumerable().First();
            }
        }

        public static int GetActiveUserId()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var user = dbContext.Users.FirstOrDefault();
                return user.Id;
            }
        }

        public static string GetActiveUserName()
        {
            using (var dbContext = new FarnahadManufacturingDbContext())
            {
                var userId = GetActiveUserId();
                return dbContext.Users.Find(userId).UserName;
            }
        }
    }
}
