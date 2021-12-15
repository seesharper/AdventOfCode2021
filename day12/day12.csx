#load "../tools.csx"

var input = File.ReadAllLines("day12/input.txt");

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


List<string> paths = new List<string>();

VisitCave(startCave, "start", new Dictionary<string, int>(caves.Select(kvp => kvp.Key).Select(k => KeyValuePair.Create(k, 0))));



private void VisitCave(Cave cave, string currentPath, Dictionary<string, int> visitorMap)
{
    currentPath += " => " + cave.Name;
    if (cave.Name == "end")
    {
        paths.Add(currentPath);
        WriteLine(currentPath);
        return;
    }

    visitorMap = IncreaseVisitCount(cave.Name);

    foreach (var edge in cave.Edges)
    {
        if (CanVisitEdge(edge))
        {
            VisitCave(edge, currentPath, visitorMap);
        }
    }

    bool CanVisitEdge(Cave edge)
    {
        if (edge.Name == "start")
        {
            return false;
        }


        if (cave.CaveType == CaveType.SmallCave && edge.CaveType == CaveType.SmallCave && edge.Edges.Count == 1 && edge.Edges[0] == cave)
        {
            return false;
        }

        else if (edge.CaveType == CaveType.SmallCave && visitorMap[edge.Name] > 0)
        {
            return false;
        }

        return true;
    }

    Dictionary<string, int> IncreaseVisitCount(string key)
    {
        var newDictionary = new Dictionary<string, int>(visitorMap.Select(kvp => KeyValuePair.Create(kvp.Key, kvp.Value)));
        newDictionary[key]++;
        return newDictionary;
    }
}



var t = paths.Distinct().ToArray().Count();

t.ShouldBe(5958);
WriteLine($"Number of paths: {t}");

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

public record Cave(string Name, CaveType CaveType, List<Cave> Edges);

public enum CaveType
{
    Start,
    End,
    BigCave,
    SmallCave
}
