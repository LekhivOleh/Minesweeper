using System.ComponentModel;

namespace Minesweeper.API.Models;

public class Cell
{
    public int Row { get; set; }
    public int Column { get; set; }
    public bool HasMine { get; set; }
    public int AdjacentMinesCount { get; set; }
    public bool IsRevealed { get; set; }
    public bool IsFlagged { get; set; }
    public bool IsQuestioned { get; set; }

    public string Display
    {
        get
        {
            if (!IsRevealed)
            {
                if (IsFlagged)
                    return "🚩";
                if (IsQuestioned)
                    return "❓";
                return "";
            }
            if (HasMine)
                return "💥";
            return AdjacentMinesCount != 0 ? AdjacentMinesCount.ToString() : "";
        }
    }

}
