var input = File.ReadLines("day2/input.txt").ToArray();


int horizontalPosition = 0;
int depth = 0;

int aim = 0;
foreach (var command in input)
{
    if (command.StartsWith("forward"))
    {
        var value = int.Parse(command.Substring(7));
        horizontalPosition += value;
        depth += aim * value;
    }

    if (command.StartsWith("down"))
    {
        var value = int.Parse(command.Substring(4));
        aim += value;
    }

    if (command.StartsWith("up"))
    {
        var value = int.Parse(command.Substring(2));
        aim -= value;
    }
}

var result = depth * horizontalPosition;

WriteLine(result);
