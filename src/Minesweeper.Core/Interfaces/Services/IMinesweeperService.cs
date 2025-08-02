using Minesweeper.Core.Models;

namespace Minesweeper.Core.Interfaces.Services;

public interface IMinesweeperService
{
    GameBoard BuildGameBoard(int rows, int columns, int minesNumber);
    Cell RevealCell(GameBoard board, int row, int column);
    Cell FlagCell(GameBoard board, int row, int column);
    Cell QuestionCell(GameBoard board, int row, int column);
    bool CheckWinCondition(GameBoard board);
}