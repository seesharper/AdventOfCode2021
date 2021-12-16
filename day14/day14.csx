#load "../tools.csx"

var input = File.ReadAllLines("day14/input.txt");
var polymerTemplate = input[0];

/*
NNCB
NCNCB   AFTER NN Mapping to C
NCNBCB  AFTER NC Mapping to B
NCNBCHB AFTER CB Mapping to H
*/



var insertionRules = new Dictionary<string, string>(input.Skip(2).Select(l => KeyValuePair.Create(l.Substring(0, 2), l.Substring(6, 1))));

for (int i = 0; i < 10; i++)
{
    polymerTemplate = ProcessTemplate(polymerTemplate);
}
var groups = polymerTemplate.GroupBy(c => c).Select(g => new { g.Key, Count = g.Count() }).OrderByDescending(g => g.Count);

//groups.Dump();

var result = groups.First().Count - groups.Last().Count;
result.ShouldBe(2223);
WriteLine($"Result after 10 steps: {result}");

//result.Dump();

private string ProcessTemplate(string template)
{
    var templatePairs = GetTemplatePairs(polymerTemplate);

    List<string> processedPairs = new List<string>();
    foreach (var templatePair in templatePairs)
    {
        var letterToBeInserted = insertionRules[templatePair];
        var processedPair = templatePair[0] + letterToBeInserted + templatePair[1];
        processedPairs.Add(processedPair);
        //processedPair.Dump("processedPair");
    }

    var newTemplate = processedPairs.Aggregate((current, next) => current + next.Substring(1));

    //newTemplate.Dump();
    return newTemplate;
}



private string[] GetTemplatePairs(string template)
{
    string[] pairs = new string[template.Length - 1];

    for (int i = 0; i < template.Length - 1; i++)
    {
        string pair = template.Substring(i, 2);
        pairs[i] = pair;
    }

    //pairs.Dump();
    return pairs;
}

