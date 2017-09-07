using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lolpremade.Models;
using Microsoft.EntityFrameworkCore;

namespace lolpremade.Data
{
    public class LolpremadeContext : DbContext
    {
        public LolpremadeContext(DbContextOptions<LolpremadeContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamInvitation> TeamInvitations { get; set; }
        public DbSet<LOLUserInfo> LOLUsersInfo { get; set; }
        public DbSet<UserPositionInTeam> UserPositionInTeam { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<UserOpinion> UserOpinions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<TournamentParticipant> TournamentParticipants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasAlternateKey(u => u.Email);
            modelBuilder.Entity<User>().HasAlternateKey(u => u.Username);
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Team>().HasAlternateKey(t => t.Name);
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<TeamInvitation>().ToTable("TeamInvitation");
            modelBuilder.Entity<LOLUserInfo>().ToTable("LOLUserInfo");
            modelBuilder.Entity<UserPositionInTeam>().ToTable("UserPositionInTeam");
            modelBuilder.Entity<Tournament>().HasAlternateKey(t => t.Name);
            modelBuilder.Entity<Tournament>().ToTable("Tournament");
            modelBuilder.Entity<UserOpinion>().ToTable("UserOpinion");
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<TournamentParticipant>().ToTable("TournamentParticipant");
        }
    }
}
