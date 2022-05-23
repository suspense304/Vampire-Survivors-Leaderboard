using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Vampire_Survivors_Leaderboard.Models
{
    public partial class vswebsiteContext : DbContext
    {
        public vswebsiteContext()
        {
        }

        public vswebsiteContext(DbContextOptions<vswebsiteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }
        public virtual DbSet<Leaderboard> Leaderboards { get; set; }
        public virtual DbSet<RunType> RunTypes { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Version> Versions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:vsleaderboard.database.windows.net,1433;Initial Catalog=vswebsite;Persist Security Info=False;User ID=vsadmin;Password=Advantage#1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Character>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Entry>(entity =>
            {
                entity.Property(e => e.DateSubmitted).HasColumnType("datetime");

                entity.Property(e => e.SurvivedTime).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.Entries)
                    .HasForeignKey(d => d.CharacterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entries_Characters");

                entity.HasOne(d => d.Stage)
                    .WithMany(p => p.Entries)
                    .HasForeignKey(d => d.StageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entries_Stage");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Entries)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entries_Users");

                entity.HasOne(d => d.VersionNavigation)
                    .WithMany(p => p.Entries)
                    .HasForeignKey(d => d.Version)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entries_Version");
            });

            modelBuilder.Entity<Leaderboard>(entity =>
            {
                entity.ToTable("Leaderboard");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.LeaderboardIdNavigation)
                    .HasForeignKey<Leaderboard>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Leaderboard_Users");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LeaderboardUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Leaderboard_Leaderboard");

                entity.HasOne(d => d.VersionNavigation)
                    .WithMany(p => p.Leaderboards)
                    .HasForeignKey(d => d.Version)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Leaderboard_Version");
            });

            modelBuilder.Entity<RunType>(entity =>
            {
                entity.ToTable("RunType");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Stage>(entity =>
            {
                entity.ToTable("Stage");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.DiscordCode).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.UserKey).IsRequired();
            });

            modelBuilder.Entity<Version>(entity =>
            {
                entity.ToTable("Version");

                entity.Property(e => e.Name).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
