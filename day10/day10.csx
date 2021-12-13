#load "../tools.csx"

var input = File.ReadAllLines("day10/input.txt").Select(l => l.Trim());

var tokenMap = new Dictionary<char, char>()
{
    {'(',')'},
    {'[',']'},
    {'{','}'},
    {'<','>'}
};

var scoreMap = new Dictionary<char, int>()
{
    {')',3},
    {']',57},
    {'}',1197},
    {'>',25137}
};

var completionScoreMap = new Dictionary<char, int>()
{
    {')',1},
    {']',2},
    {'}',3},
    {'>',4}
};

var corruptedLines = new List<string>();

int syntaxErrorScore = 0;
foreach (var line in input)
{
    Stack<char> stack = new Stack<char>();

    for (int i = 0; i < line.Length; i++)
    {
        if (tokenMap.ContainsKey(line[i]))
        {
            stack.Push(line[i]);
        }
        else
        {
            var currentEndToken = line[i];
            var lastStartToken = stack.Pop();
            var expectedEndToken = tokenMap[lastStartToken];
            if (currentEndToken != expectedEndToken)
            {
                syntaxErrorScore += scoreMap[currentEndToken];
                corruptedLines.Add(line);
            }
        }
    }
}

syntaxErrorScore.ShouldBe(341823);
WriteLine($"Syntax error score: {syntaxErrorScore}");

var incompleteLines = input.Except(corruptedLines).ToArray();
var totalCompletionScores = new List<long>();

foreach (var line in incompleteLines)
{
    Stack<char> stack = new Stack<char>();

    for (int i = 0; i < line.Length; i++)
    {
        if (tokenMap.ContainsKey(line[i]))
        {
            stack.Push(line[i]);
        }
        else
        {
            var lastStartToken = stack.Pop();
        }
    }

    var completionScores = new List<long>();
    while (stack.Count > 0)
    {
        var openingToken = stack.Pop();
        var closinToken = tokenMap[openingToken];
        completionScores.Add(completionScoreMap[closinToken]);
    }

    var totalCompletionScore = 0L;
    for (int i = 0; i < completionScores.Count; i++)
    {
        totalCompletionScore = (totalCompletionScore * 5) + completionScores[i];
    }
    totalCompletionScores.Add(totalCompletionScore);
}

var middleScoreIndex = (totalCompletionScores.Count - 1) / 2;
var middleScore = totalCompletionScores.OrderBy(i => i).ToArray()[middleScoreIndex];

middleScore.ShouldBe(2801302861);

WriteLine($"Middle score: {middleScore}");