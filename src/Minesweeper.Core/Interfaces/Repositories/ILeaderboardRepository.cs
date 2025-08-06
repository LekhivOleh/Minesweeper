using Minesweeper.Core.Models;

namespace Minesweeper.Core.Interfaces.Repositories;

public interface ILeaderboardRepository
{
    Task<IList<LeaderboardEntry>> GetAllAsync();
    Task<LeaderboardEntry> AddAsync(LeaderboardEntry entry);
    Task SaveChangesAsync();
}