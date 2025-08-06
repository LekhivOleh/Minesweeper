using Microsoft.EntityFrameworkCore;
using Minesweeper.Core.Interfaces.Repositories;
using Minesweeper.Core.Models;
using Minesweeper.Persistence;

namespace Minesweeper.Infrastructure.Repositories;

public class LeaderboardRepository(AppDbContext context) : ILeaderboardRepository
{
    public async Task<LeaderboardEntry> AddAsync(LeaderboardEntry entry)
    {
        if (entry == null)
            throw new ArgumentNullException(nameof(entry));

        await context.LeaderboardEntries.AddAsync(entry);
        await SaveChangesAsync();

        return entry;
    }

    public async Task<IList<LeaderboardEntry>> GetAllAsync()
    {
        var entries = await context.LeaderboardEntries
            .OrderBy(e => e.BestTime)
            .ToListAsync();
        return entries;
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}