#load "../tools.csx"


var map = ReadMap();

var rowCount = map.GetLength(0);
var columnCount = map.GetLength(1);
var lowPoints = GetLowPoints(ReadMap());
var riskLevel = lowPoints.Select(p => map[p.Row, p.Column] + 1).Sum();

riskLevel.ShouldBe(491);

WriteLine($"Risk level is {riskLevel}");

var threeLargestBasinsMultiplied = lowPoints.Select(lp => GetBasinLocation(lp)).OrderByDescending(i => i).Take(3).Aggregate((current, next) => current * next);

threeLargestBasinsMultiplied.ShouldBe(1075536);

WriteLine($"Three largest basins multiplied : {threeLargestBasinsMultiplied}");

private int GetBasinLocation(Location location)
{
    var basinLocations = new HashSet<Location>();
    AddBasinLocations(location.Row, location.Column);
    basinLocations.Add(location);

    void AddBasinLocations(int row, int column)
    {
        AddRight(row, column);
        AddLeft(row, column);
        AddTop(row, column);
        AddBottom(row, column);
    }

    void AddRight(int row, int column)
    {
        if (column + 1 == columnCount || map[row, column + 1] == 9 || map[row, column + 1] <= map[row, column])
        {
            return;
        }
        else
        {
            basinLocations.Add(new Location(row, column + 1));
            AddBasinLocations(row, column + 1);
        }
    }

    void AddLeft(int row, int column)
    {
        if (column - 1 < 0 || map[row, column - 1] == 9 || map[row, column - 1] <= map[row, column])
        {
            return;
        }
        else
        {
            basinLocations.Add(new Location(row, column - 1));
            AddBasinLocations(row, column - 1);
        }
    }

    void AddTop(int row, int column)
    {
        if (row - 1 < 0 || map[row - 1, column] == 9 || map[row - 1, column] <= map[row, column])
        {
            return;
        }
        else
        {
            basinLocations.Add(new Location(row - 1, column));
            AddBasinLocations(row - 1, column);
        }
    }

    void AddBottom(int row, int column)
    {
        if (row + 1 == rowCount || map[row + 1, column] == 9 || map[row + 1, column] <= map[row, column])
        {
            return;
        }
        else
        {
            basinLocations.Add(new Location(row + 1, column));
            AddBasinLocations(row + 1, column);
        }
    }

    return basinLocations.Count();
}


private int[] GetAdjacentNumbers(int row, int column)
{
    var adjacentNumbers = new List<int>();

    if (row == 0)
    {
        if (column == 0)
        {
            Add(Right(), Bottom());
        }
        else if (column == columnCount - 1)
        {
            Add(Left(), Bottom());
        }
        else
        {
            Add(Left(), Right(), Bottom());
        }
    }

    else if (row == rowCount - 1)
    {
        if (column == 0)
        {
            Add(Top(), Right());
        }
        else if (column == columnCount - 1)
        {
            Add(Left(), Top());
        }
        else
        {
            Add(Left(), Right(), Top());
        }
    }

    else
    {
        if (column == 0)
        {
            Add(Top(), Bottom(), Right());
        }
        else if (column == columnCount - 1)
        {
            Add(Top(), Left(), Bottom());
        }
        else
        {
            Add(Left(), Right(), Top(), Bottom());
        }
    }

    int Left() => map[row, column - 1];
    int Right() => map[row, column + 1];
    int Top() => map[row - 1, column];
    int Bottom() => map[row + 1, column];
    void Add(params int[] values) => adjacentNumbers.AddRange(values);

    return adjacentNumbers.ToArray();
}

private Location[] GetLowPoints(int[,] map)
{
    var lowPoints = new List<Location>();

    for (int r = 0; r < map.GetLength(0); r++)
    {
        for (int c = 0; c < map.GetLength(1); c++)
        {
            var adjacentNumbers = GetAdjacentNumbers(r, c);
            if (adjacentNumbers.All(an => an > map[r, c]))
            {
                lowPoints.Add(new Location(r, c));
            }
        }
    }
    return lowPoints.ToArray();
}


private int[,] ReadMap()
{
    var lines = File.ReadAllLines("day9/input.txt");

    int rowLength = lines[0].Length;
    int[,] map = new int[lines.Length, rowLength];

    for (int r = 0; r < lines.Length; r++)
    {
        for (int c = 0; c < rowLength; c++)
        {
            map[r, c] = int.Parse(lines[r][c].ToString());
        }
    }
    return map;
}

public record Location(int Row, int Column);