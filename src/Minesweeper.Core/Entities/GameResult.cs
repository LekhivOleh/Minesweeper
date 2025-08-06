namespace Minesweeper.Core.Models;

public class GameResult
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public TimeOnly Time { get; set; }
    public DateTime PlayedAt { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
    public int Mines { get; set; }
    public bool IsWin { get; set; }
}
