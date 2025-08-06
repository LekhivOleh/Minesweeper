using Minesweeper.Core.Interfaces.Repositories;
using Minesweeper.Core.Interfaces.Services;
using Minesweeper.Core.Models;

namespace Minesweeper.Infrastructure.Services;

public class LeaderboardService(ILeaderboardRepository leaderboardRepository) : ILeaderboardService
{
    public async Task<LeaderboardEntry> AddAsync(LeaderboardEntry entry)
    {
        if (entry == null)
            throw new ArgumentNullException(nameof(entry));

        await leaderboardRepository.AddAsync(entry);

        return entry;
    }

    public async Task<IEnumerable<LeaderboardEntry>> GetAllAsync()
    {
        var entries = await leaderboardRepository.GetAllAsync();
        return entries;
    }

    public async Task SaveChangesAsync()
    {
        await leaderboardRepository.SaveChangesAsync();
    }
}