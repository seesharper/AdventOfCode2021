var input = File.ReadLines("day2/input.txt").ToArray();


int horizontalPosition = 0;
int depth = 0;
foreach (var command in input)
{
    if (command.StartsWith("forward"))
    {
        var value = int.Parse(command.Substring(7));
        horizontalPosition += value;
    }

    if (command.StartsWith("down"))
    {
        var value = int.Parse(command.Substring(4));
        depth += value;
    }

    if (command.StartsWith("up"))
    {
        var value = int.Parse(command.Substring(2));
        depth -= value;
    }
}

var result = depth * horizontalPosition;

WriteLine(result);
