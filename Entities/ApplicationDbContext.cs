
using Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Duel> Duels { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<UserTournament> UserTournaments { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<TournamentStats> TournamentStats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserTournament>()
                .HasKey(ut => new { ut.UserId, ut.TournamentId });

            modelBuilder.Entity<UserTournament>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.UserTournaments)
                .HasForeignKey(ut => ut.UserId);

            modelBuilder.Entity<UserTournament>()
                .HasOne(ut => ut.Tournament)
                .WithMany(t => t.UserTournaments)
                .HasForeignKey(ut => ut.TournamentId);

            modelBuilder.Entity<Duel>()
                .HasOne(d => d.Tournament)
                .WithMany(t => t.TournamentDuels)
                .HasForeignKey(d => d.TournamentId);

            modelBuilder.Entity<TournamentStats>()
                .HasKey(ts => ts.Id);

            
        }


    }
}
