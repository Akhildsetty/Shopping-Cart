using Ecommerce_api.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_api.Data
{
    public class databaseContext:DbContext
    {
        public databaseContext(DbContextOptions<databaseContext> options):base(options)
        {

        }

        public DbSet<RegisterModel> Registration { get; set; }
        public DbSet<CountryCodeModel> CountryCode { get; set; }
    }
}
