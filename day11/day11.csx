#load "../tools.csx"

var input = File.ReadAllLines("day11/input.txt");

const int GridSize = 10;

Octopus[,] map = new Octopus[GridSize, GridSize];

for (int r = 0; r < GridSize; r++)
{
    for (int c = 0; c < GridSize; c++)
    {
        var octopus = new Octopus(int.Parse(input[r][c].ToString()), r, c);
        map[r, c] = octopus;
    }
}

PrintMap();

FindAdjacents();

static int totalFlashes = 0;

var flashesAfter100Steps = 0;

var firstStepWhereAllFlashes = 0;

for (int i = 0; i < 500; i++)
{
    Reset();
    var f = Step();

    if (i == 100)
    {
        flashesAfter100Steps = totalFlashes;
    }

    if (f == 100)
    {
        firstStepWhereAllFlashes = i + 1;
    }
    totalFlashes += f;
}

flashesAfter100Steps.ShouldBe(1773);
WriteLine($"Total number of flashes after 100 steps: {flashesAfter100Steps}");

firstStepWhereAllFlashes.ShouldBe(494);
WriteLine($"First step where all flashes: {firstStepWhereAllFlashes}");


private int Step()
{
    for (int r = 0; r < GridSize; r++)
    {
        for (int c = 0; c < GridSize; c++)
        {
            map[r, c].EnergyLevel++;
        }
    }

    int stepFlashes = 0;
    int flashes = 0;
    flashes = Flash();
    while (flashes > 0)
    {
        stepFlashes += flashes;
        flashes = Flash();
    }
    return stepFlashes;
}

private int Flash()
{
    int flashes = 0;
    for (int r = 0; r < GridSize; r++)
    {
        for (int c = 0; c < GridSize; c++)
        {
            if (map[r, c].EnergyLevel > 9)
            {
                map[r, c].EnergyLevel = 0;
                map[r, c].HasFlashed = true;
                flashes++;
                foreach (var adjacent in map[r, c].Adjacents)
                {
                    if (!adjacent.HasFlashed)
                    {
                        adjacent.EnergyLevel++;
                    }

                }
            }
        }
    }
    if (flashes == 100)
    {

    }
    return flashes;
}

private void PrintMap()
{
    var sb = new StringBuilder();
    for (int r = 0; r < GridSize; r++)
    {
        for (int c = 0; c < GridSize; c++)
        {
            sb.Append(map[r, c].EnergyLevel);
        }
        sb.AppendLine();
    }

    WriteLine(sb.ToString());
}

private void Reset()
{
    for (int r = 0; r < GridSize; r++)
    {
        for (int c = 0; c < GridSize; c++)
        {
            map[r, c].Reset();
        }
    }
}

private void FindAdjacents()
{
    for (int r = 0; r < GridSize; r++)
    {
        for (int c = 0; c < GridSize; c++)
        {
            if (r == 0)
            {
                if (c == 0)
                {
                    AddRight(r, c);
                    AddBottomRight(r, c);
                    AddBottom(r, c);
                }
                else if (c == (GridSize - 1))
                {
                    AddLeft(r, c);
                    AddBottom(r, c);
                    AddBottomLeft(r, c);
                }
                else
                {
                    AddLeft(r, c);
                    AddBottomLeft(r, c);
                    AddBottom(r, c);
                    AddBottomRight(r, c);
                    AddRight(r, c);
                }
            }
            else if (r == (GridSize - 1))
            {
                if (c == 0)
                {
                    AddTop(r, c);
                    AddTopRight(r, c);
                    AddRight(r, c);
                }
                else if (c == (GridSize - 1))
                {
                    AddLeft(r, c);
                    AddTopLeft(r, c);
                    AddTop(r, c);
                }

                else
                {
                    AddLeft(r, c);
                    AddTopLeft(r, c);
                    AddTop(r, c);
                    AddTopRight(r, c);
                    AddRight(r, c);
                }
            }
            else
            {
                if (c == 0)
                {
                    AddTop(r, c);
                    AddTopRight(r, c);
                    AddRight(r, c);
                    AddBottomRight(r, c);
                    AddBottom(r, c);
                }
                else if (c == (GridSize - 1))
                {
                    AddTop(r, c);
                    AddTopLeft(r, c);
                    AddLeft(r, c);
                    AddBottomLeft(r, c);
                    AddBottom(r, c);
                }
                else
                {
                    AddRight(r, c);
                    AddLeft(r, c);
                    AddBottom(r, c);
                    AddBottomRight(r, c);
                    AddBottomLeft(r, c);
                    AddTop(r, c);
                    AddTopRight(r, c);
                    AddTopLeft(r, c);
                }
            }
        }
    }

    void AddRight(int r, int c) => map[r, c].Adjacents.Add(map[r, c + 1]);
    void AddLeft(int r, int c) => map[r, c].Adjacents.Add(map[r, c - 1]);
    void AddBottom(int r, int c) => map[r, c].Adjacents.Add(map[r + 1, c]);
    void AddBottomRight(int r, int c) => map[r, c].Adjacents.Add(map[r + 1, c + 1]);
    void AddBottomLeft(int r, int c) => map[r, c].Adjacents.Add(map[r + 1, c - 1]);
    void AddTop(int r, int c) => map[r, c].Adjacents.Add(map[r - 1, c]);
    void AddTopRight(int r, int c) => map[r, c].Adjacents.Add(map[r - 1, c + 1]);
    void AddTopLeft(int r, int c) => map[r, c].Adjacents.Add(map[r - 1, c - 1]);
}

public record Octopus()
{
    public Octopus(int energyLevel, int row, int column) : this()
    {
        Adjacents = new List<Octopus>();
        EnergyLevel = energyLevel;
        Row = row;
        Column = column;
    }

    public int Row { get; set; }

    public int Column { get; set; }

    public int EnergyLevel { get; set; }
    public List<Octopus> Adjacents { get; set; }
    public bool HasFlashed { get; set; }
    public void Reset() => HasFlashed = false;
}

