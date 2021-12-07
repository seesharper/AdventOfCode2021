#load "../tools.csx"

using System.Text.RegularExpressions;

var input = File.ReadAllLines("day4/input.txt");
var drawnNumbers = input[0].Split(",").Select(int.Parse);

var boards = ReadBoards(input.Skip(1).ToArray());

var wonBoards = new List<Board>();


foreach (var drawnNumber in drawnNumbers)
{
    foreach (var board in boards)
    {
        if (wonBoards.Contains(board))
        {
            continue;
        }

        board.Play(drawnNumber);

        if (board.Check())
        {
            var sum = board.SumOfAllUnmarkedNumbers();
            var result = board.SumOfAllUnmarkedNumbers() * drawnNumber;
            board.WinningNumber = drawnNumber;
            wonBoards.Add(board);
        }
    }
}

var firstWinningBoard = wonBoards.First();
var firstWinningBoardScore = firstWinningBoard.SumOfAllUnmarkedNumbers() * firstWinningBoard.WinningNumber;

firstWinningBoardScore.ShouldBe(27027);

WriteLine($"First winning board score: {firstWinningBoardScore}");

var lastWinningBoard = wonBoards.Last();
var lastWinningBoardScore = lastWinningBoard.SumOfAllUnmarkedNumbers() * lastWinningBoard.WinningNumber;

lastWinningBoardScore.ShouldBe(36975);

WriteLine($"Last winning board score: {lastWinningBoardScore}");

public class Board
{
    private readonly Number[,] boardNumbers;

    public Board(Number[,] boardNumbers)
    {
        this.boardNumbers = boardNumbers;
    }

    public int WinningNumber { get; set; }

    public void Play(int number)
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (boardNumbers[x, y].Value == number)
                {
                    boardNumbers[x, y].IsMarked = true;
                }
            }
        }
    }

    public bool Check()
    {
        for (int i = 0; i < 5; i++)
        {
            int markedCount = 0;
            for (int y = 0; y < 5; y++)
            {
                if (boardNumbers[i, y].IsMarked)
                {
                    markedCount++;
                }
                if (markedCount == 5)
                {
                    return true;
                }
            }
        }

        for (int i = 0; i < 5; i++)
        {
            int markedCount = 0;
            for (int y = 0; y < 5; y++)
            {
                if (boardNumbers[y, i].IsMarked)
                {
                    markedCount++;
                }
                if (markedCount == 5)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public int SumOfAllUnmarkedNumbers()
    {
        int sum = 0;

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                if (!boardNumbers[x, y].IsMarked)
                {
                    sum += boardNumbers[x, y].Value;
                }
            }
        }

        return sum;
    }
}


public class Number
{
    public Number(int value)
    {
        Value = value;
    }

    public bool IsMarked { get; set; }
    public int Value { get; }
}


public List<Board> ReadBoards(string[] input)
{
    var boards = new List<Board>();

    var lineCount = input.Length;
    var startPosition = 1;
    while (lineCount > startPosition)
    {
        var board = ReadBoard(input, startPosition);
        boards.Add(board);
        startPosition += 6;
    }

    return boards;
}

private Board ReadBoard(string[] input, int startPosition)
{
    Number[,] boardNumbers = new Number[5, 5];

    for (int i = startPosition; i < startPosition + 5; i++)
    {

        var numbers = Regex.Matches(input[i], @"\d+").Select(m => m.Value).Select(int.Parse).ToArray();
        for (int y = 0; y < 5; y++)
        {
            boardNumbers[i - startPosition, y] = new Number(numbers[y]);
        }
    }

    return new Board(boardNumbers);
}
