namespace save_point.Models
{
    /// <summary>
    /// Tracks one game's progress on one platform.
    /// A game can have several of these (e.g. played on both PC and Switch).
    /// </summary>
    public class PlatformEntry
    {
        /// <summary>Primary key.</summary>
        public int Id { get; set; }

        /// <summary>Foreign key to the owning <see cref="Game"/>.</summary>
        public int GameId { get; set; }

        /// <summary>
        /// Owning game. Populated by EF Core when loaded from the database;
        /// initialized to null! because it is required in the schema but not
        /// set manually when adding entries through Game.PlatformEntries.
        /// </summary>
        public Game Game { get; set; } = null!;

        /// <summary>Platform the game is played on (e.g. "PC", "PS5").</summary>
        public required string Platform { get; set; }

        /// <summary>Current play status on this platform.</summary>
        public GameStatus Status { get; set; }

        /// <summary>Whether the game has been completed on this platform.</summary>
        public bool Completed { get; set; }

        /// <summary>Date of completion, or null when not completed.</summary>
        public DateTime? CompletionDate { get; set; }

        /// <summary>Rating from 0 to 10 in 0.5 steps, or null when not rated.</summary>
        public double? Rating { get; set; }

        /// <summary>Total hours played, or null when not tracked.</summary>
        public int? HoursPlayed { get; set; }

        /// <summary>Free-form notes, or null when none.</summary>
        public string? Remarks { get; set; }
    }
}
