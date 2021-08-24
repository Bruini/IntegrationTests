using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NintendoGameStore.Core.Models;
using NintendoGameStore.Infrastructure.Data.Config.Mappings;

namespace NintendoGameStore.Infrastructure.Data.Config
{
    public class StoreDBContext : DbContext
    {
        public StoreDBContext(DbContextOptions<StoreDBContext> options): base(options)
        {
          
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryDBMapping());
            modelBuilder.ApplyConfiguration(new GameDBMapping());
        }
    }
}
