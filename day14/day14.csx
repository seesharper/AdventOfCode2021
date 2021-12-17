#load "../tools.csx"

var input = File.ReadAllLines("day14/sample.txt");
var polymerTemplate = input[0];

/*
NNCB
NCNCB   AFTER NN Mapping to C
NCNBCB  AFTER NC Mapping to B
NCNBCHB AFTER CB Mapping to H
*/



var insertionRules = new Dictionary<string, string>(input.Skip(2).Select(l => KeyValuePair.Create(l.Substring(0, 2), l.Substring(6, 1))));

var chars = insertionRules.Keys.SelectMany(k => k).Distinct(); ;

var map = GetInitialTemplatePairs(polymerTemplate);
var letterMap = new Dictionary<char, long>(chars.Select(c => KeyValuePair.Create(c, 0L)));

foreach (var letter in polymerTemplate)
{
    letterMap[letter]++;
}



for (int i = 0; i < 40; i++)
{
    ProcessTemplate(map);
    WriteLine(i);
}


letterMap.Dump();


// foreach (var kvp in map)
// {
//     letterMap[kvp.Key[0]] += kvp.Value;
//     letterMap[kvp.Key[1]] += kvp.Value;
// }

// letterMap.Dump();

// var groups = polymerTemplate.GroupBy(c => c).Select(g => new { g.Key, Count = g.Count() }).OrderByDescending(g => g.Count);

// //groups.Dump();

// var result = groups.First().Count - groups.Last().Count;
// result.ShouldBe(2223);
// WriteLine($"Result after 10 steps: {result}");

//result.Dump();

WriteLine("Done");


private Dictionary<string, long> CreateEmptyMap()
{
    return new Dictionary<string, long>(insertionRules.Keys.Select(k => KeyValuePair.Create(k, 0L)));
}

private void ProcessTemplate(Dictionary<string, long> valuePairMap)
{
    var producedValuePairs = CreateEmptyMap();
    var removedValuePairs = CreateEmptyMap();

    foreach (var templatePair in valuePairMap)
    {
        for (long i = 0; i < templatePair.Value; i++)
        {
            var letterToBeInserted = insertionRules[templatePair.Key];
            producedValuePairs[templatePair.Key[0] + letterToBeInserted]++;
            producedValuePairs[letterToBeInserted + templatePair.Key[1]]++;
            removedValuePairs[templatePair.Key]++;
            letterMap[letterToBeInserted[0]]++;
        }
    }

    foreach (var valuePair in valuePairMap)
    {
        valuePairMap[valuePair.Key] += producedValuePairs[valuePair.Key];
        valuePairMap[valuePair.Key] -= removedValuePairs[valuePair.Key];
    }
    WriteLine("");
}

private Dictionary<string, long> GetInitialTemplatePairs(string template)
{
    var map = CreateEmptyMap();
    for (int i = 0; i < template.Length - 1; i++)
    {
        string pair = template.Substring(i, 2);
        map[pair] += 1;
    }
    return map;
}
