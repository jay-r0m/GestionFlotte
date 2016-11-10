using GestionFlotte.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionFlotte.Data
{
    public class FlotteContext : DbContext
    {
        public FlotteContext(DbContextOptions<FlotteContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Maitrise> Maitrises { get; set; }
        public DbSet<Marin> Marins { get; set; }
        public DbSet<TypeBateau> TypesBateaux { get; set; }
        public DbSet<Poste> Postes { get; set; }
        public DbSet<Bateau> Bateaux { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Maitrise>().ToTable("Maitrise");
            modelBuilder.Entity<Marin>().ToTable("Marin");
            modelBuilder.Entity<TypeBateau>().ToTable("TypeBateau");
            modelBuilder.Entity<Poste>().ToTable("Poste");
            modelBuilder.Entity<Bateau>().ToTable("Bateau");
        }

    }
}
