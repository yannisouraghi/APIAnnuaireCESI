using Microsoft.EntityFrameworkCore;
using APIAnnuaire.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIAnnuaire
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Sites> Sites { get; set; }
        public DbSet<Services> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Configurez ici votre connexion SQLite
                optionsBuilder.UseSqlite("Data Source=data.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>()
                .HasOne(e => e.Sites)
                .WithMany()
                .HasForeignKey(e => e.SiteId);

            modelBuilder.Entity<Employees>()
                .HasOne(e => e.Services)
                .WithMany()
                .HasForeignKey(e => e.ServiceId);
        }
    }
}
