using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarnahadManufacturing.Data
{
    public class FarnahadManufacturingDbContext : DbContext
    {
        public FarnahadManufacturingDbContext() : base("FarnahadManufacturingConnectionString")
        {
        }

    }
}
