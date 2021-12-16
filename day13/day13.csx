#load "../tools.csx"


var input = File.ReadAllLines("day13/input.txt");

var coordinates = input.Select(l => l.Trim()).Where(l => !string.IsNullOrWhiteSpace(l) & !l.StartsWith("fold")).Select(l => l.Split(",")).Select(p => new { x = int.Parse(p[0]), y = int.Parse(p[1]) }).ToArray();
char[,] originalPage = CreatePage(coordinates.Max(c => c.x) + 1, coordinates.Max(c => c.y) + 1);
var foldInstructions = input.Where(l => l.StartsWith("fold")).Select(l => l[11..]).ToArray();


foreach (var coordinate in coordinates)
{
    originalPage[coordinate.x, coordinate.y] = '#';
}

PrintPage(originalPage);


char[,] page = originalPage;
for (int i = 0; i < foldInstructions.Length; i++)
{
    var index = int.Parse(foldInstructions[i].Substring(2));
    if (foldInstructions[i].StartsWith("y"))
    {
        page = FoldUp(page, index);
    }
    else
    {
        page = FoldLeft(page, index);
    }

    if (i == 0)
    {
        var dotsAfterFirstFold = CountDots(page);
        dotsAfterFirstFold.ShouldBe(751);
        WriteLine($"Number of dots after first fold: {dotsAfterFirstFold}");
    }
}

private char[,] CreatePage(int xCount, int yCount)
{
    var page = new char[xCount, yCount];
    for (int x = 0; x < xCount; x++)
    {
        for (int y = 0; y < yCount; y++)
        {
            page[x, y] = '.';
        }
    }
    return page;
}
private char[,] FoldUp(char[,] page, int index)
{
    var xCount = page.GetLength(0);
    var yCount = page.GetLength(1);
    char[,] upperHalfPage = CreatePage(xCount, index);

    for (int y = 0; y < index; y++)
    {
        for (int x = 0; x < xCount; x++)
        {
            upperHalfPage[x, y] = page[x, y];
        }
    }

    for (int y = index; y < yCount; y++)
    {
        for (int x = 0; x < xCount; x++)
        {
            if (page[x, y] == '#')
            {
                upperHalfPage[x, yCount - y - 1] = page[x, y];
            }
        }
    }

    PrintPage(upperHalfPage);
    return upperHalfPage;
}


private char[,] FoldLeft(char[,] page, int index)
{
    var xCount = page.GetLength(0);
    var yCount = page.GetLength(1);
    char[,] leftHalfPage = CreatePage(index, yCount);

    for (int y = 0; y < yCount; y++)
    {
        for (int x = 0; x < index; x++)
        {
            leftHalfPage[x, y] = page[x, y];
        }
    }

    for (int y = 0; y < yCount; y++)
    {
        for (int x = index; x < xCount; x++)
        {
            if (page[x, y] == '#')
            {
                leftHalfPage[xCount - x - 1, y] = page[x, y];
            }
        }
    }

    PrintPage(leftHalfPage);
    return leftHalfPage;
}




private int CountDots(char[,] page)
{
    var xCount = page.GetLength(0);
    var yCount = page.GetLength(1);


    int count = 0;
    for (int y = 0; y < yCount; y++)
    {
        for (int x = 0; x < xCount; x++)
        {
            if (page[x, y] == '#')
            {
                count++;
            }
        }
    }

    return count;
}


private void PrintPage(char[,] page)
{
    var xCount = page.GetLength(0);
    var yCount = page.GetLength(1);

    var sb = new StringBuilder();
    for (int y = 0; y < yCount; y++)
    {
        for (int x = 0; x < xCount; x++)
        {
            sb.Append(page[x, y]);
        }
        sb.AppendLine();
    }
    WriteLine(sb.ToString());
}