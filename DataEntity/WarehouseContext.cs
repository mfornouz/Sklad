
using Microsoft.EntityFrameworkCore;

namespace DataEntity
{
    public class WarehouseContext : DbContext
    {
        public DbSet<Material> Materials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=(localdb)\\MSSQLLocalDB;" +
                    "Initial Catalog=Warehouse2026;" +
                    "Integrated Security=True;" +
                    "TrustServerCertificate=True;"
                ).UseLazyLoadingProxies();
            }
        }
    }
}

