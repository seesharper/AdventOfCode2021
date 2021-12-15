#load "../tools.csx"

var input = File.ReadAllLines("day12/sample.txt");

var caves = new Dictionary<string, Cave>();

foreach (var line in input)
{
    var route = line.Split("-").ToArray();
    if (!caves.ContainsKey(route[0]))
    {
        caves.Add(route[0], new Cave(route[0], GetCaveType(route[0]), new List<Cave>()));
    }
    if (!caves.ContainsKey(route[1]))
    {
        caves.Add(route[1], new Cave(route[1], GetCaveType(route[1]), new List<Cave>()));
    }

    if (route[0] != "start")
    {
        caves[route[1]].Edges.Add(caves[route[0]]);
    }


    caves[route[0]].Edges.Add(caves[route[1]]);



}

var startCave = caves.Single(kvp => kvp.Key == "start").Value;

VisitCave(startCave, "start");

private void VisitCave(Cave cave, string currentPath = "")
{
    currentPath += $" => {cave.Name}";
    WriteLine(currentPath);
    

    cave.VisitCount++;
    foreach (var edge in cave.Edges)
    {
        if (cave.CaveType == CaveType.SmallCave && edge.CaveType == CaveType.SmallCave)
        {
            return;
        }
        else if (edge.CaveType == CaveType.SmallCave && edge.VisitCount > 0)
        {
            return;
        }

        else if (edge.Name == "end")
        {
            currentPath += " => end";
            foreach (var kvp in caves)
            {
                kvp.Value.VisitCount = 0;
            }

            return;
        }

        else
        {

            VisitCave(edge, currentPath);
        }
    }
}


WriteLine("Done");

private CaveType GetCaveType(string name)
{
    if (name == "start")
    {
        return CaveType.Start;
    }
    if (name == "end")
    {
        return CaveType.End;
    }
    if (char.IsLower(name[0]))
    {
        return CaveType.SmallCave;
    }
    return CaveType.BigCave;
}

public record Cave(string Name, CaveType CaveType, List<Cave> Edges)
{
    public int VisitCount { get; set; }

}

public enum CaveType
{
    Start,
    End,
    BigCave,
    SmallCave
}





