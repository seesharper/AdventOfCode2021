#load "../tools.csx"

var lowPoints = GetLowPoints(ReadMap());
var riskLevel = lowPoints.Select(p => p + 1).Sum();

riskLevel.ShouldBe(491);

WriteLine($"Risk level is {riskLevel}");

private int[] GetAdjacentNumbers(int[,] map, int row, int column)
{
    var adjacentNumbers = new List<int>();
    var rowCount = map.GetLength(0);
    var columnCount = map.GetLength(1);


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

private int[] GetLowPoints(int[,] map)
{
    var lowPoints = new List<int>();


    for (int r = 0; r < map.GetLength(0); r++)
    {
        for (int c = 0; c < map.GetLength(1); c++)
        {
            var adjacentNumbers = GetAdjacentNumbers(map, r, c);
            if (adjacentNumbers.All(an => an > map[r, c]))
            {
                lowPoints.Add(map[r, c]);
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