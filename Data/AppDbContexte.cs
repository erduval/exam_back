using Examen.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Examen.Data
{
    public class AppDbContexte: IdentityDbContext<AppUser>
    {
        public AppDbContexte (DbContextOptions<AppDbContexte> options) : base(options)
        {
        }

        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<InterventionTechnicien> InterventionTechniciens { get; set; }

        protected override void OnModelCreating (ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<InterventionTechnicien>()
                .HasKey(it => new { it.InterventionId, it.TechnicienId });

            builder.Entity<InterventionTechnicien>()
                .HasOne(it => it.Intervention)
                .WithMany(i => i.TechnicienLinks)
                .HasForeignKey(it => it.InterventionId);

            builder.Entity<InterventionTechnicien>()
                .HasOne(it => it.Technicien)
                .WithMany(t => t.TechnicienInterventions)
                .HasForeignKey(it => it.TechnicienId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Intervention>()
                .HasOne(i => i.Client)
                .WithMany(u => u.ClientInterventions)
                .HasForeignKey(i => i.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ServiceType>()
                .HasIndex(s => s.Name)
                .IsUnique();
        }
    }
}
