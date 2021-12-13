#load "../tools.csx"

var input = File.ReadAllLines("day10/input.txt").Select(l => l.Trim());

Dictionary<char, char> tokenMap = new Dictionary<char, char>()
{
    {'(',')'},
    {'[',']'},
    {'{','}'},
    {'<','>'}
};

Dictionary<char, int> scoreMap = new Dictionary<char, int>()
{
    {')',3},
    {']',57},
    {'}',1197},
    {'>',25137}
};

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
            }
        }
    }
}

syntaxErrorScore.ShouldBe(341823);
WriteLine($"Syntax error score: {syntaxErrorScore}");
