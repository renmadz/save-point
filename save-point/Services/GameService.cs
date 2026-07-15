using Microsoft.EntityFrameworkCore;
using save_point.Data;
using save_point.Models;

namespace save_point.Services
{
    /// <summary>
    /// CRUD operations for <see cref="Game"/> records.
    /// Each method uses its own short-lived context, so returned entities
    /// are detached. Not-found cases return false/null; database failures throw.
    /// </summary>
    public class GameService
    {
        /// <summary>Adds a new game and returns it with its generated Id.</summary>
        public async Task<Game> AddGameAsync(Game game)
        {
            using var db = new SavePointContext();
            db.Games.Add(game);
            await db.SaveChangesAsync();
            return game;
        }

        /// <summary>
        /// Updates a game's own fields (not its platform entries).
        /// Returns false when the game no longer exists.
        /// </summary>
        public async Task<bool> UpdateGameAsync(Game game)
        {
            using var db = new SavePointContext();
            if (!await db.Games.AnyAsync(g => g.Id == game.Id))
            {
                return false;
            }

            db.Entry(game).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deletes a game; its platform entries are removed by cascade.
        /// Returns false when the game no longer exists.
        /// </summary>
        public async Task<bool> DeleteGameAsync(int gameId)
        {
            using var db = new SavePointContext();
            var game = await db.Games.FindAsync(gameId);
            if (game is null)
            {
                return false;
            }

            db.Games.Remove(game);
            await db.SaveChangesAsync();
            return true;
        }

        /// <summary>Gets one game with its platform entries, or null if not found.</summary>
        public async Task<Game?> GetGameAsync(int id)
        {
            using var db = new SavePointContext();
            return await db.Games
                .Include(g => g.PlatformEntries)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        /// <summary>Gets all games with their platform entries, untracked.</summary>
        public async Task<List<Game>> GetAllGamesAsync()
        {
            using var db = new SavePointContext();
            return await db.Games
                .Include(g => g.PlatformEntries)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
