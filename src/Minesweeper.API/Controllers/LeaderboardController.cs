using Microsoft.AspNetCore.Mvc;
using Minesweeper.Persistence;

namespace Minesweeper.API.Controllers
{
    public class LeaderboardController(AppDbContext context) : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("GameStartTime", DateTime.UtcNow.ToString("O"));

            var entries = context.LeaderboardEntries
                .OrderBy(e => e.BestTime)
                .ToList();
            return View(entries);
        }
    }
}