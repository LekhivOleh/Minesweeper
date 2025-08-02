namespace Minesweeper.API.DTOs;

public class RevealRequest
{
    public int Row { get; set; }
    public int Col { get; set; }
}

public class FlagRequest
{
    public int Row { get; set; }
    public int Col { get; set; }
}

public class QuestionRequest
{
    public int Row { get; set; }
    public int Col { get; set; }
}