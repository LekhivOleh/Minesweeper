namespace Minesweeper.Core.Models;

public class User
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }

    public ICollection<GameResult> GameResults { get; set; } = new List<GameResult>();
}