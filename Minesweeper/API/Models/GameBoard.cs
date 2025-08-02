namespace Minesweeper.API.Models;

public class GameBoard
{
    public int Rows { get; set; }
    public int Columns { get; set; }
    public int MinesNumber { get; set; }
    public int MinesLeft { get; set; }
    public bool IsGameOver { get; set; }
    public bool IsWin { get; set; }
    public required List<List<Cell>> Grid { get; set; }
}
