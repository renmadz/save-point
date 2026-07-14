namespace save_point.Models
{
    /// <summary>
    /// Play status of a game on a specific platform.
    /// Stored in the database as an integer, so the explicit values
    /// must never be changed once data exists.
    /// </summary>
    public enum GameStatus
    {
        Backlog = 0,
        Playing = 1,
        Completed = 2,
        Dropped = 3,
        OnHold = 4
    }
}
