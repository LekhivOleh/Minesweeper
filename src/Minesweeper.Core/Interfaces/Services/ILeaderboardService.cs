using Minesweeper.Core.Models;

namespace Minesweeper.Core.Interfaces.Services;

public interface ILeaderboardService
{
    Task<IEnumerable<LeaderboardEntry>> GetAllAsync();
    Task<LeaderboardEntry> AddAsync(LeaderboardEntry entry);
    public Task SaveChangesAsync();
}