namespace Minesweeper.Core.Models;
public class LeaderboardEntry
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public TimeOnly BestTime { get; set; }
}
