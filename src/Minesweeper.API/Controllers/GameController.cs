using Microsoft.AspNetCore.Mvc;
using Minesweeper.API.DTOs;
using Minesweeper.Core.Extensions;
using Minesweeper.Core.Interfaces.Services;
using Minesweeper.Core.Models;

namespace Minesweeper.API.Controllers;

public class GameController(IMinesweeperService minesweeperService, ILeaderboardService leaderboardService) : Controller
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

            var startTimeStr = HttpContext.Session.GetString("GameStartTime");
            TimeOnly bestTime = TimeOnly.MinValue;
            if (DateTime.TryParse(startTimeStr, null, System.Globalization.DateTimeStyles.RoundtripKind, out var startTime))
            {
                var elapsed = DateTime.UtcNow - startTime;
                bestTime = TimeOnly.FromTimeSpan(elapsed);
            }
            var username = "Anonymous"; // Replace with actual username logic

            // Save to leaderboard
            var entry = new LeaderboardEntry
            {
                Id = Guid.NewGuid(),
                Username = username,
                BestTime = bestTime
            };
            leaderboardService.AddAsync(entry);
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