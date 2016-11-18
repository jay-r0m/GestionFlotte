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
        public DbSet<Marin> Marins { get; set; }
        public DbSet<TypeBateau> TypesBateaux { get; set; }
        public DbSet<Poste> Postes { get; set; }
        public DbSet<Bateau> Bateaux { get; set; }
        public DbSet<RoleAssignment> RoleAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Marin>().ToTable("Marin");
            modelBuilder.Entity<TypeBateau>().ToTable("TypeBateau");
            modelBuilder.Entity<Poste>().ToTable("Poste");
            modelBuilder.Entity<Bateau>().ToTable("Bateau");

            modelBuilder.Entity<RoleAssignment>().ToTable("RoleAssignment");
            modelBuilder.Entity<RoleAssignment>()
                .HasKey(c => new { c.RoleID, c.MarinID });

        }

    }
}
