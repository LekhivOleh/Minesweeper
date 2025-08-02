using Minesweeper.API.Interfaces.Services;
using Minesweeper.API.Models;

namespace Minesweeper.API.Services;

public class MinesweeperService : IMinesweeperService
{
    private readonly static (int, int)[] allDirections = new (int, int)[]
    {
        (-1, -1), (-1, 0), (-1, 1), ( 0, -1), ( 0, 1), ( 1, -1), ( 1, 0), ( 1, 1)
    };
    private readonly static (int, int)[] cardinalDirections = new (int, int)[] 
    { 
        (-1, 0), (0, 1), (1, 0), (0, -1) 
    };

    public GameBoard BuildGameBoard(int rows, int columns, int minesNumber)
    {
        GameBoard board = new GameBoard
        {
            Rows = rows,
            Columns = columns,
            MinesNumber = minesNumber,
            MinesLeft = minesNumber,
            IsGameOver = false,
            IsWin = false,
            Grid = new List<List<Cell>>()
        };

        CreateGameBoard(board);
        GenerateMines(board);
        CalculateAdjacentMines(board);
        return board;
    }

    private void CreateGameBoard(GameBoard gameBoard)
    {
        for (int r = 0; r < gameBoard.Rows; r++)
        {
            List<Cell> boardRow = new List<Cell>();
            for (int c = 0; c < gameBoard.Columns; c++)
            {
                boardRow.Add(new Cell
                {
                    Row = r,
                    Column = c,
                    HasMine = false,
                    AdjacentMinesCount = 0,
                    IsRevealed = false,
                    IsFlagged = false
                });
            }
            gameBoard.Grid.Add(boardRow);
        }
        return;
    }

    private void GenerateMines(GameBoard gameBoard)
    {
        var gen = new Random();
        int placedMines = 0;
        int rows = gameBoard.Rows;
        int cols = gameBoard.Columns;

        HashSet<(int, int)> usedPositions = new HashSet<(int, int)>();

        while (placedMines < gameBoard.MinesNumber)
        {
            int row = gen.Next(rows);
            int column = gen.Next(cols);

            if (!usedPositions.Contains((row, column)))
            {
                gameBoard.Grid[row][column].HasMine = true;
                usedPositions.Add((row, column));
                placedMines++;
            }
        }
        return;
    }

    private void CalculateAdjacentMines(GameBoard board)
    {
        for (int r = 0; r < board.Rows; r++)
        {
            for (int c = 0; c < board.Columns; c++)
            {
                if (board.Grid[r][c].HasMine)
                {
                    continue;
                }

                int count = 0;
                foreach (var (dx, dy) in allDirections)
                {
                    int newRow = r + dx;
                    int newCol = c + dy;

                    if (newRow >= 0 && newRow < board.Rows &&
                        newCol >= 0 && newCol < board.Columns &&
                        board.Grid[newRow][newCol].HasMine)
                    {
                        count++;
                    }
                }
                board.Grid[r][c].AdjacentMinesCount = count;
            }
        }
        return;
    }

    public Cell RevealCell(GameBoard board, int row, int column)
    {
        if (!IsCellInBounds(board, row, column))
        {
            throw new ArgumentOutOfRangeException("Cell coordinates are out of bounds.");
        }

        if (board.IsGameOver)
        {
            return board.Grid[row][column];
        }

        Cell cell = board.Grid[row][column];

        if (cell.IsRevealed || cell.IsFlagged || cell.IsQuestioned)
        {
            return cell;
        }

        cell.IsRevealed = true;

        // Option 1: Cell is a mine. End the game.
        if (cell.HasMine)
        {
            board.IsGameOver = true;
            board.IsWin = false;
            return cell;
        }

        // Option 2: Cell is not a mine. Check adjacent cells.
        if (cell.AdjacentMinesCount == 0)
        {
            foreach (var (dx, dy) in allDirections)
            {
                int newRow = row + dx;
                int newCol = column + dy;

                if (newRow >= 0 && newRow < board.Rows &&
                    newCol >= 0 && newCol < board.Columns &&
                    !board.Grid[newRow][newCol].IsRevealed)
                {
                    RevealCell(board, newRow, newCol);
                }
            }
        }

        if (CheckWinCondition(board))
        {
            board.IsGameOver = true;
            board.IsWin = true;
        }
        return cell;
    }

    public Cell FlagCell(GameBoard board, int row, int column)
    {
        if (!IsCellInBounds(board, row, column))
        {
            throw new ArgumentOutOfRangeException("Cell coordinates are out of bounds.");
        }

        if (board.IsGameOver)
        {
            throw new Exception("Game is over");
        }

        Cell cell = board.Grid[row][column];

        if (cell.IsRevealed)
        {
            return cell;
        }

        cell.IsFlagged = !cell.IsFlagged;

        board.MinesLeft = board.MinesNumber - board.Grid.SelectMany(r => r).Count(c => c.IsFlagged);

        return cell;
    }

    public Cell QuestionCell(GameBoard board, int row, int column)
    {
        if (!IsCellInBounds(board, row, column))
        {
            throw new ArgumentOutOfRangeException("Cell coordinates are out of bounds.");
        }

        if (board.IsGameOver)
        {
            throw new Exception("Game is over");
        }

        Cell cell = board.Grid[row][column];

        if (cell.IsRevealed)
        {
            return cell;
        }

        if (!cell.IsRevealed && !cell.IsFlagged)
            cell.IsQuestioned = !cell.IsQuestioned;

        return cell;
    }

    public bool CheckWinCondition(GameBoard board)
    {
        for (int r = 0; r < board.Rows; r++)
        {
            for (int c = 0; c < board.Columns; c++)
            {
                Cell cell = board.Grid[r][c];
                if (!cell.HasMine && !cell.IsRevealed)
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    private bool IsCellInBounds(GameBoard board, int row, int column)
    {
        return row >= 0 && row < board.Rows && column >= 0 && column < board.Columns;
    }
}