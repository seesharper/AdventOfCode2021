using System.Text.RegularExpressions;

// https://www.geeksforgeeks.org/number-integral-points-two-points/

// https://www.geeksforgeeks.org/euclidean-algorithms-basic-and-extended/

// https://www.geeksforgeeks.org/check-if-two-given-line-segments-intersect/


const int diagramSize = 1000;

var lines = ReadLines();

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

private void DumpDiagram()
{
    var sb = new StringBuilder();
    for (int x = 0; x < diagramSize; x++)
    {
        for (int y = 0; y < diagramSize; y++)
        {
            if (diagram[x, y] == 0)
            {
                sb.Append(".");
            }
            else
            {
                sb.Append(diagram[x, y]);
            }
        }
        sb.AppendLine();
    }
    WriteLine(sb.ToString());
}


// var horizontalAndVerticalLines = lines.Where(l => l.Start.x == l.End.x || l.Start.y == l.End.y).ToArray();

// var test = GetIntersectingLines(horizontalAndVerticalLines).Distinct().ToArray();

//DumpDiagram();
WriteLine("done");


private Line[] ReadLines()
{
    var lines = new List<Line>();
    var input = File.ReadAllLines("day5/input.txt");
    foreach (var lineData in input)
    {
        lines.Add(ReadLine(lineData));
    }
    return lines.ToArray();
}

private Line ReadLine(string lineData)
{
    var numbers = Regex.Matches(lineData, @"\d+").Select(m => m.Value).Select(int.Parse).ToArray();
    var start = new Point(numbers[0], numbers[1]);
    var end = new Point(numbers[2], numbers[3]);
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


    if (start.X != end.X && start.Y != end.Y)
    {
        var distance = Math.Abs(start.Y - end.Y);
        for (int i = 0; i <= distance; i++)
        {
            var point = new Point(start.X - i, start.Y + i);
        }
    }


    return new Line(linePoints.ToArray());
}

private Line[] GetIntersectingLines(Line[] lines)
{
    var intersectingLines = new List<Line>();
    foreach (var line1 in lines)
    {
        foreach (var line2 in lines)
        {
            if (line1 == line2)
            {
                continue;
            }

            // if (doIntersect(line1.Start, line1.End, line2.Start, line2.End))
            // {
            //     intersectingLines.Add(line1);
            //     intersectingLines.Add(line2);
            // }
        }
    }
    return intersectingLines.ToArray();
}

private record Intersection(Line Line1, Line Line2);


private record Point(int X, int Y);

private record Line(Point[] Points);

// // Given three collinear points p, q, r, the function checks if
// // point q lies on line segment 'pr'
// static Boolean onSegment(Point p, Point q, Point r)
// {
//     if (q.x <= Math.Max(p.x, r.x) && q.x >= Math.Min(p.x, r.x) &&
//         q.y <= Math.Max(p.y, r.y) && q.y >= Math.Min(p.y, r.y))
//         return true;

//     return false;
// }

// // To find orientation of ordered triplet (p, q, r).
// // The function returns following values
// // 0 --> p, q and r are collinear
// // 1 --> Clockwise
// // 2 --> Counterclockwise
// static int orientation(Point p, Point q, Point r)
// {
//     // See https://www.geeksforgeeks.org/orientation-3-ordered-points/
//     // for details of below formula.
//     int val = (q.y - p.y) * (r.x - q.x) -
//             (q.x - p.x) * (r.y - q.y);

//     if (val == 0) return 0; // collinear

//     return (val > 0) ? 1 : 2; // clock or counterclock wise
// }

// // The main function that returns true if line segment 'p1q1'
// // and 'p2q2' intersect.
// static Boolean doIntersect(Point p1, Point q1, Point p2, Point q2)
// {
//     // Find the four orientations needed for general and
//     // special cases
//     int o1 = orientation(p1, q1, p2);
//     int o2 = orientation(p1, q1, q2);
//     int o3 = orientation(p2, q2, p1);
//     int o4 = orientation(p2, q2, q1);

//     // General case
//     if (o1 != o2 && o3 != o4)
//         return true;

//     // Special Cases
//     // p1, q1 and p2 are collinear and p2 lies on segment p1q1
//     if (o1 == 0 && onSegment(p1, p2, q1)) return true;

//     // p1, q1 and q2 are collinear and q2 lies on segment p1q1
//     if (o2 == 0 && onSegment(p1, q2, q1)) return true;

//     // p2, q2 and p1 are collinear and p1 lies on segment p2q2
//     if (o3 == 0 && onSegment(p2, p1, q2)) return true;

//     // p2, q2 and q1 are collinear and q1 lies on segment p2q2
//     if (o4 == 0 && onSegment(p2, q1, q2)) return true;

//     return false; // Doesn't fall in any of the above cases
// }

// // Driver code
// public static void Main(String[] args)
// {
//     Point p1 = new Point(1, 1);
//     Point q1 = new Point(10, 1);
//     Point p2 = new Point(1, 2);
//     Point q2 = new Point(10, 2);

//     if (doIntersect(p1, q1, p2, q2))
//         Console.WriteLine("Yes");
//     else
//         Console.WriteLine("No");

//     p1 = new Point(10, 1); q1 = new Point(0, 10);
//     p2 = new Point(0, 0); q2 = new Point(10, 10);
//     if (doIntersect(p1, q1, p2, q2))
//         Console.WriteLine("Yes");
//     else
//         Console.WriteLine("No");

//     p1 = new Point(-5, -5); q1 = new Point(0, 0);
//     p2 = new Point(1, 1); q2 = new Point(10, 10); ;
//     if (doIntersect(p1, q1, p2, q2))
//         Console.WriteLine("Yes");
//     else
//         Console.WriteLine("No");
// }

// /* This code contributed by PrinciRaj1992 */