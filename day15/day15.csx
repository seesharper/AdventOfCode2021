#load "../tools.csx"

var input = File.ReadAllLines("day15/sample.txt");
Vertex[,] map = new Vertex[input.Length, input.Length];

for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < input.Length; x++)
    {
        var test2 = int.Parse(input[x][y].ToString());
        map[y, x] = new Vertex(y, x, new List<Vertex>()) { Distance = int.Parse(input[x][y].ToString()), Visited = false, DistanceFromSource = int.MaxValue };
    }
}

for (int x = 0; x < map.GetLength(0); x++)
{
    for (int y = 0; y < map.GetLength(1); y++)
    {
        if (x == map.GetLength(0) - 1)
        {
            if (y != map.GetLength(1) - 1)
            {
                map[x, y].Edges.Add(map[x, y + 1]);
            }
        }
        else
        {
            if (y != map.GetLength(1) - 1)
            {
                map[x, y].Edges.Add(map[x + 1, y]);
                map[x, y].Edges.Add(map[x, y + 1]);
            }
            else
            {
                map[x, y].Edges.Add(map[x + 1, y]);
            }
        }
    }
}

var startVertex = map[0, 0];
startVertex.DistanceFromSource = 0;
Visit(startVertex);

WriteLine("test");

private void Visit(Vertex vertex)
{
    if (vertex.Visited == true)
    {
        return;
    }

    foreach (var edge in vertex.Edges.Where(e => !e.Visited))
    {
        var tentativeDistance = vertex.DistanceFromSource + edge.Distance;
        if (tentativeDistance < edge.DistanceFromSource)
        {
            edge.DistanceFromSource = tentativeDistance;
        }
    }
    vertex.Visited = true;

    if (vertex.X == 9 && vertex.Y == 9)
    {

    }

    if (vertex.Edges.Count == 0)
    {
        return;
    }



    var edgeWithLowestTentativeDistance = vertex.Edges.OrderBy(e => e.DistanceFromSource).First();
    Visit(edgeWithLowestTentativeDistance);

}





public record Vertex(int X, int Y, List<Vertex> Edges)
{
    public int Distance { get; set; }

    public bool Visited { get; set; }

    public int DistanceFromSource { get; set; }
}
