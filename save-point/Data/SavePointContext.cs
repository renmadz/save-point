using Microsoft.EntityFrameworkCore;
using save_point.Models;

namespace save_point.Data
{
    /// <summary>
    /// EF Core database context for Save Point.
    /// Uses a SQLite database file stored beside the executable.
    /// </summary>
    public class SavePointContext : DbContext
    {
        /// <summary>All tracked games.</summary>
        public DbSet<Game> Games => Set<Game>();

        /// <summary>All per-platform entries across all games.</summary>
        public DbSet<PlatformEntry> PlatformEntries => Set<PlatformEntry>();

        /// <summary>
        /// Full path to the SQLite database file, next to the executable.
        /// </summary>
        public static string DatabasePath { get; } =
            Path.Combine(AppContext.BaseDirectory, "save-point.db");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DatabasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Game: one game has many platform entries.
            modelBuilder.Entity<Game>(game =>
            {
                game.HasKey(g => g.Id);
                game.Property(g => g.Title).IsRequired();

                game.HasMany(g => g.PlatformEntries)
                    .WithOne(pe => pe.Game)
                    .HasForeignKey(pe => pe.GameId)
                    .IsRequired()
                    // Deleting a game also deletes its platform entries.
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PlatformEntry>(entry =>
            {
                entry.HasKey(pe => pe.Id);
                entry.Property(pe => pe.Platform).IsRequired();
            });
        }
    }
}
