using Microsoft.EntityFrameworkCore;
using save_point.Data;
using save_point.Models;

namespace save_point.Services
{
    /// <summary>
    /// CRUD operations for <see cref="PlatformEntry"/> records.
    /// Each method uses its own short-lived context, so returned entities
    /// are detached. Not-found cases return false/null; database failures throw.
    /// </summary>
    public class PlatformEntryService
    {
        /// <summary>
        /// Adds a new entry and returns it with its generated Id,
        /// or null when the referenced game does not exist.
        /// </summary>
        public async Task<PlatformEntry?> AddPlatformEntryAsync(PlatformEntry entry)
        {
            using var db = new SavePointContext();
            if (!await db.Games.AnyAsync(g => g.Id == entry.GameId))
            {
                return null;
            }

            db.PlatformEntries.Add(entry);
            await db.SaveChangesAsync();
            return entry;
        }

        /// <summary>
        /// Updates an entry. Returns false when the entry no longer exists
        /// or its GameId does not reference a real game.
        /// </summary>
        public async Task<bool> UpdatePlatformEntryAsync(PlatformEntry entry)
        {
            using var db = new SavePointContext();
            if (!await db.PlatformEntries.AnyAsync(pe => pe.Id == entry.Id) ||
                !await db.Games.AnyAsync(g => g.Id == entry.GameId))
            {
                return false;
            }

            db.Entry(entry).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        /// <summary>Deletes an entry. Returns false when it no longer exists.</summary>
        public async Task<bool> DeletePlatformEntryAsync(int id)
        {
            using var db = new SavePointContext();
            var entry = await db.PlatformEntries.FindAsync(id);
            if (entry is null)
            {
                return false;
            }

            db.PlatformEntries.Remove(entry);
            await db.SaveChangesAsync();
            return true;
        }

        /// <summary>Gets all entries for one game, untracked.</summary>
        public async Task<List<PlatformEntry>> GetPlatformEntriesForGameAsync(int gameId)
        {
            using var db = new SavePointContext();
            return await db.PlatformEntries
                .Where(pe => pe.GameId == gameId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
