using Xunit;
using Minesweeper.API.Interfaces.Services;
using Minesweeper.API.Models;
using Minesweeper.API.Services;

namespace Minesweeper.Tests;
public class MinesweeperServiceTests
{
    private readonly IMinesweeperService minesweeperService = new MinesweeperService();

    [Fact]
    public void BuildGameBoard_CreatesCorrectDimentions()
    {
        int rows = 5;
        int columns = 4;
        int mines = 3;

        var board = minesweeperService.BuildGameBoard(rows, columns, mines);

        Assert.NotNull(board);
        Assert.Equal(rows, board.Rows);
        Assert.Equal(columns, board.Columns);
        Assert.Equal(mines, board.MinesNumber);
        foreach (var row in board.Grid)
        {
            Assert.Equal(columns, row.Count);
        }
    }

    [Fact]
    public void BuildGameBoard_GenerateRightMinesAmount()
    {
        int rows = 5;
        int columns = 5;
        int mines = 10;

        var board = minesweeperService.BuildGameBoard(rows, columns, mines);
        int mineCount = board.Grid.SelectMany(c => c).Count(c => c.HasMine);

        Assert.Equal(mines, mineCount);
    }

    [Fact]
    public void RevealCell_RevealsCellCorrectly()
    {
        int rows = 5;
        int columns = 5;
        int mines = 3;

        var board = minesweeperService.BuildGameBoard(rows, columns, mines);
        var cell = board.Grid[0][0];
        var revealedCell = minesweeperService.RevealCell(board, cell.Row, cell.Column);

        Assert.True(revealedCell.IsRevealed);
        Assert.Equal(cell.Row, revealedCell.Row);
        Assert.Equal(cell.Column, revealedCell.Column);
    }

    [Fact]
    public void RevealCell_DoesNotRevealFlaggedCell()
    {
        int rows = 5;
        int columns = 5;
        int mines = 3;

        var board = minesweeperService.BuildGameBoard(rows, columns, mines);
        var cell = board.Grid[0][0];
        minesweeperService.FlagCell(board, cell.Row, cell.Column);
        var revealedCell = minesweeperService.RevealCell(board, cell.Row, cell.Column);

        Assert.False(revealedCell.IsRevealed);
    }

    [Fact]
    public void RevealCell_EndGameWhenMineIsRevealed()
    {
        int rows = 5;
        int columns = 5;
        int mines = 3;

        var board = minesweeperService.BuildGameBoard(rows, columns, mines);
        var mineCell = board.Grid.SelectMany(c => c).FirstOrDefault(c => c.HasMine);
        Assert.NotNull(mineCell);
        var revealedCell = minesweeperService.RevealCell(board, mineCell.Row, mineCell.Column);

        Assert.True(board.IsGameOver);
        Assert.False(board.IsWin);
    }

    [Fact]
    public void RevealCell_WinWhenNoMinesLeft()
    {
        int rows = 5;
        int columns = 5;
        int mines = 3;

        var board = minesweeperService.BuildGameBoard(rows, columns, mines);

        board.Grid.SelectMany(c => c)
            .Where(c => !c.HasMine)
            .ToList()
            .ForEach(c => minesweeperService.RevealCell(board, c.Row, c.Column));

        Assert.True(minesweeperService.CheckWinCondition(board));
    }
}
