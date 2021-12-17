#load "../tools.csx"

var input = File.ReadAllLines("day14/input.txt");
var polymerTemplate = input[0];
var insertionRules = new Dictionary<string, string>(input.Skip(2).Select(l => KeyValuePair.Create(l.Substring(0, 2), l.Substring(6, 1))));
var chars = insertionRules.Keys.SelectMany(k => k).Distinct();

var result = ProcessSteps(10);
result.ShouldBe(2223);
WriteLine($"Result after 10 steps : {result}");

result = ProcessSteps(40);
result.ShouldBe(2566282754493);
WriteLine($"Result after 40 steps : {result}");

WriteLine("Done");

private long ProcessSteps(int numberOfSteps)
{
    var letterMap = new Dictionary<char, long>(chars.Select(c => KeyValuePair.Create(c, 0L)));

    foreach (var letter in polymerTemplate)
    {
        letterMap[letter]++;
    }

    var map = GetInitialTemplatePairs(polymerTemplate);

    for (int i = 0; i < numberOfSteps; i++)
    {
        ProcessTemplate(map, letterMap);
    }

    var orderedLetterMap = letterMap.OrderByDescending(k => k.Value);
    var result = orderedLetterMap.First().Value - orderedLetterMap.Last().Value;
    return result;
}


private Dictionary<string, long> CreateEmptyMap()
    => new(insertionRules.Keys.Select(k => KeyValuePair.Create(k, 0L)));

private void ProcessTemplate(Dictionary<string, long> valuePairMap, Dictionary<char, long> letterMap)
{
    var producedValuePairs = CreateEmptyMap();
    var removedValuePairs = CreateEmptyMap();

    foreach (var templatePair in valuePairMap)
    {
        var letterToBeInserted = insertionRules[templatePair.Key];
        producedValuePairs[templatePair.Key[0] + letterToBeInserted] += templatePair.Value;
        producedValuePairs[letterToBeInserted + templatePair.Key[1]] += templatePair.Value;
        removedValuePairs[templatePair.Key] += templatePair.Value;
        letterMap[letterToBeInserted[0]] += templatePair.Value;
    }

    foreach (var valuePair in valuePairMap)
    {
        valuePairMap[valuePair.Key] += producedValuePairs[valuePair.Key];
        valuePairMap[valuePair.Key] -= removedValuePairs[valuePair.Key];
    }
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
