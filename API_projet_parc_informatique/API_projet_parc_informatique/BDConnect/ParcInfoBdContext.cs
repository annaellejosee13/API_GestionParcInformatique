using API_projet_parc_informatique.Models;
using Microsoft.EntityFrameworkCore;

namespace API_projet_parc_informatique.BD
{
    public class ParcInfoBdContext : DbContext
    {
        public ParcInfoBdContext(DbContextOptions<ParcInfoBdContext> options) : base(options)
        {
        }

        public DbSet<Parc> Parcs { get; set; }
        public DbSet<Salle> Salles { get; set; }
        public DbSet<Poste> Postes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Remontees> Remontees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurer la relation entre Parc et Salle
            modelBuilder.Entity<Salle>()
                .HasOne(s => s.Parc)
                .WithMany(p => p.Salles)
                .HasForeignKey(s => s.Id_parc)
                .OnDelete(DeleteBehavior.Cascade); // Gestion de la suppression en cascade si nécessaire

            // Configurer la relation entre Salle et Poste
            modelBuilder.Entity<Poste>()
                .HasOne(p => p.Salle)
                .WithMany(s => s.Postes)
                .HasForeignKey(p => p.Id_salle)
                .OnDelete(DeleteBehavior.Cascade); // Gestion de la suppression en cascade si nécessaire

            // Configurer la relation entre Poste et User
            //modelBuilder.Entity<Poste>()
            //    .HasOne(p => p.User)
            //    .WithMany(u => u.Postes)
            //    .HasForeignKey(p => p.IdUser)
            //    .OnDelete(DeleteBehavior.SetNull); // En cas de suppression de l'utilisateur, les postes deviennent sans utilisateur (ou utilisez Cascade selon le besoin)

            // Configurer la relation entre Ticket et Poste
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Poste)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.Id_poste)
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade des tickets liés à un poste

            // Configurer la relation entre Ticket et User
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.Id_user)
                .OnDelete(DeleteBehavior.SetNull); // En cas de suppression de l'utilisateur, les tickets deviennent sans utilisateur (ou utilisez Cascade selon le besoin)

            // Configurer la relation entre Remontees et poste
            modelBuilder.Entity<Remontees>()
                .HasOne(r => r.Poste)
                .WithMany(p => p.Remontees)
                .HasForeignKey(r => r.Id_poste)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurer la relation entre Remontees et poste
            modelBuilder.Entity<Poste>()
                .HasMany(p => p.Remontees)
                .WithOne(r => r.Poste)
                .HasForeignKey(r => r.Id_poste)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
