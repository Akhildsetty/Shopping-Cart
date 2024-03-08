using Ecommerce_api.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_api.Data
{
    public class databaseContext:DbContext
    {
        public databaseContext(DbContextOptions<databaseContext> options):base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<CountryCode> CountryCode { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<OtpValidation> OtpValidation { get; set; }
    }
}
