
using Microsoft.EntityFrameworkCore;

namespace DataEntity
{
    public class WarehouseContext : DbContext
    {
        public DbSet<Material> Materials { get; set; }

        public DbSet<UnitOfMeasure> UnitsOfMeasure { get; set; }

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

            public void ClearAndReseed()
        {
            Database.ExecuteSqlRaw("DELETE FROM [Materials]");
            Database.ExecuteSqlRaw("DELETE FROM [UnitsOfMeasure]");
            Database.ExecuteSqlRaw("DELETE FROM [Pallets]");

            Database.ExecuteSqlRaw("DBCC CHECKIDENT ('[Materials]', RESEED, 0)");
            Database.ExecuteSqlRaw("DBCC CHECKIDENT ('[UnitsOfMeasure]', RESEED, 0)");
            Database.ExecuteSqlRaw("DBCC CHECKIDENT ('[Pallets]', RESEED, 0)");


        }

            public void Seed()
        {
            // Pokud již v databázi existují jakékoliv jednotky nebo materiály, metoda skončí a nic nepřepíše
            if (UnitsOfMeasure.Any() || Materials.Any())
            {
                return;
            }

            // Vytvoření jednotek měření
            var ks = new UnitOfMeasure { Description = "ks" };
            var kg = new UnitOfMeasure { Description = "kg" };
            var m3 = new UnitOfMeasure { Description = "m3" };

            // Vytvoření materiálů s navázáním na jednotku 'ks' přes navigační vlastnost
            var sroub = new Material
            {
                Name = "Šroub",
                SafetyStock = 1000,
                PalletCapacity = 2000,
                UnitOfMeasure = ks
            };

            var hrebik = new Material
            {
                Name = "Hřebík",
                SafetyStock = 1500,
                PalletCapacity = 1000,
                UnitOfMeasure = ks
            };

            // Vložení entit do příslušných DbSetů
            UnitsOfMeasure.AddRange(ks, kg, m3);
            Materials.AddRange(sroub, hrebik);

            // Jednorázové uložení všech změn do databáze
            SaveChanges();
        
    }

    }
    
}

