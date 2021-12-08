#load "../tools.csx"
using System.Text.RegularExpressions;
const int diagramSize = 1000;
var lines = ReadLines(onlyHorizontalAndVerticalLines: true);
var points = GetIntersectingPoints(lines);

points.ShouldBe(6311);

WriteLine($"Number of intersecting points (Horizontal and Vertical): {points}");

var allLines = ReadLines(onlyHorizontalAndVerticalLines: false);

points = GetIntersectingPoints(allLines);

points.ShouldBe(19929);

WriteLine($"Number of intersecting points (Horizontal, Vertical and Diagonal): {points}");


private int GetIntersectingPoints(Line[] lines)
{
    int[,] diagram = new int[diagramSize, diagramSize];

    foreach (var line in lines)
    {
        foreach (var point in line.Points)
        {
            diagram[point.X, point.Y]++;
        }
    }

    int points = 0;
    for (int x = 0; x < diagramSize; x++)
    {
        for (int y = 0; y < diagramSize; y++)
        {
            if (diagram[x, y] >= 2)
            {
                points++;
            }
        }
    }
    return points;
}

private Line[] ReadLines(bool onlyHorizontalAndVerticalLines)
{
    var lines = new List<Line>();
    var input = File.ReadAllLines("day5/input.txt");
    foreach (var lineData in input)
    {
        lines.Add(ReadLine(lineData, onlyHorizontalAndVerticalLines));
    }
    return lines.ToArray();
}

private Line ReadLine(string lineData, bool onlyHorizontalAndVerticalLines)
{
    var numbers = Regex.Matches(lineData, @"\d+").Select(m => m.Value).Select(int.Parse).ToArray();
    var start = new Point(numbers[0], numbers[1]);
    var end = new Point(numbers[2], numbers[3]);
    return CreateLine(start, end, onlyHorizontalAndVerticalLines);
}

Line CreateLine(Point start, Point end, bool onlyHorizontalAndVerticalLines)
{
    var linePoints = new List<Point>();
    if (start.Y == end.Y)
    {
        var distance = Math.Abs(start.X - end.X);
        int x = 0;
        if (start.X < end.X)
        {
            x = start.X;
        }
        else
        {
            x = end.X;
        }

        for (int i = 0; i <= distance; i++)
        {
            var point = new Point(x + i, start.Y);
            linePoints.Add(point);
        }
    }

    if (start.X == end.X)
    {
        var distance = Math.Abs(start.Y - end.Y);
        int y = 0;
        if (start.Y < end.Y)
        {
            y = start.Y;
        }
        else
        {
            y = end.Y;
        }

        for (int i = 0; i <= distance; i++)
        {
            var point = new Point(start.X, y + i);
            linePoints.Add(point);
        }
    }


    if (start.X != end.X && start.Y != end.Y && !onlyHorizontalAndVerticalLines)
    {
        if (start.X > end.X)
        {
            var tmp = start;
            start = end;
            end = tmp;
        }

        var distance = Math.Abs(start.Y - end.Y);

        for (int i = 0; i <= distance; i++)
        {
            if (start.Y > end.Y)
            {
                var point = new Point(start.X + i, start.Y - i);
                linePoints.Add(point);
            }
            else
            {
                var point = new Point(start.X + i, start.Y + i);
                linePoints.Add(point);
            }
        }
    }


    return new Line(linePoints.ToArray());
}


private record Point(int X, int Y);

private record Line(Point[] Points);

