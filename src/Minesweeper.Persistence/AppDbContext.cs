using Microsoft.EntityFrameworkCore;
using Minesweeper.Core.Models;

namespace Minesweeper.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<GameResult> GameResults => Set<GameResult>();
    public DbSet<LeaderboardEntry> LeaderboardEntries => Set<LeaderboardEntry>();
}