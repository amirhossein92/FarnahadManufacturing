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
            return 3;
        }
    }
}
