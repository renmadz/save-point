namespace save_point.Models
{
    /// <summary>
    /// A video game tracked by the application.
    /// A game itself holds only identity info; per-platform progress
    /// lives in its <see cref="PlatformEntry"/> records.
    /// </summary>
    public class Game
    {
        /// <summary>Primary key.</summary>
        public int Id { get; set; }

        /// <summary>Display title of the game.</summary>
        public required string Title { get; set; }

        /// <summary>
        /// Path to the cover art image on disk, or null when no cover has been set.
        /// </summary>
        public string? CoverArtPath { get; set; }

        /// <summary>
        /// Platform entries belonging to this game (one game, many entries).
        /// </summary>
        public ICollection<PlatformEntry> PlatformEntries { get; set; } = new List<PlatformEntry>();
    }
}
