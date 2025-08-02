using Microsoft.AspNetCore.Mvc;
using Minesweeper.API.DTOs;
using Minesweeper.API.Interfaces.Services;
using Minesweeper.API.Models;
using Minesweeper.API.Services;
using Minesweeper.Extensions;

namespace Minesweeper.API.Controllers;

public class GameController(IMinesweeperService minesweeperService) : Controller
{
    private const string SessionKey = "GameBoard";

    public IActionResult Index()
    {
        var board = minesweeperService.BuildGameBoard(9, 9, 10);
        HttpContext.Session.SetObject(SessionKey, board);

        return View(board);
    }

    [HttpPost]
    public IActionResult RevealCell([FromBody] RevealRequest request)
    {
        var board = HttpContext.Session.GetObject<GameBoard>("GameBoard");
        if (board == null)
            return BadRequest("Game board not found in session.");

        minesweeperService.RevealCell(board, request.Row, request.Col);
        HttpContext.Session.SetObject("GameBoard", board);

        if (minesweeperService.CheckWinCondition(board))
        {
            board.IsWin = true;
            board.IsGameOver = true;
            HttpContext.Session.SetObject("GameBoard", board);
        }

        return PartialView("_GameBoard", board);
    }

    [HttpPost]
    public IActionResult FlagCell([FromBody] FlagRequest request)
    {
        var board = HttpContext.Session.GetObject<GameBoard>("GameBoard");
        if (board == null)
            return BadRequest("Game board not found in session.");

        if (board.MinesLeft == 0 && !board.Grid[request.Row][request.Col].IsFlagged)
        {
            return PartialView("_GameBoard", board);
        }

        minesweeperService.FlagCell(board, request.Row, request.Col);
        HttpContext.Session.SetObject("GameBoard", board);

        return PartialView("_GameBoard", board);
    }

    [HttpPost]
    public IActionResult QuestionCell([FromBody] QuestionRequest request)
    {
        var board = HttpContext.Session.GetObject<GameBoard>("GameBoard");
        if (board == null)
            return BadRequest("Game board not found in session.");

        if (board.MinesLeft == 0 && !board.Grid[request.Row][request.Col].IsFlagged && !board.Grid[request.Row][request.Col].IsQuestioned)
        {
            return PartialView("_GameBoard", board);
        }

        minesweeperService.QuestionCell(board, request.Row, request.Col);
        HttpContext.Session.SetObject("GameBoard", board);

        return PartialView("_GameBoard", board);
    }

    [HttpGet]
    public IActionResult GameAlert()
    {
        var board = HttpContext.Session.GetObject<GameBoard>("GameBoard");
        if (board == null)
            return Content("");

        return PartialView("_GameAlert", board);
    }
}