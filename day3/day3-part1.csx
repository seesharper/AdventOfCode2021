#load "../tools.csx"

var input = File.ReadLines("day3/input.txt").ToArray().Select(e => Convert.ToInt32(e, 2));
int bitCount = 12;
int gamma = 0;
int epsilon = 0;

int[] mostCommonBits = new int[bitCount];
foreach (var number in input)
{
    for (int i = 0; i < bitCount; i++)
    {
        if (number.IsSet(i))
        {
            mostCommonBits[i]++;
        }
    }
}


for (int i = 0; i < bitCount; i++)
{
    if (mostCommonBits[i] > input.Count() / 2)
    {
        gamma = gamma.SetBit(i);
    }
    else
    {
        epsilon = epsilon.SetBit(i);
    }
}

var powerConsumption = gamma * epsilon;

Debug.Assert(powerConsumption == 2261546);

WriteLine($"Power consumption : {powerConsumption}");

