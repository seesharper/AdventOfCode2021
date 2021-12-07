#load "../tools.csx"

var input = File.ReadLines("day3/input.txt").ToArray().Select(e => Convert.ToInt32(e, 2)).ToArray();
int bitCount = 12;
int gamma = 0;
int epsilon = 0;

var commonBits = input.GetMostCommonBits(bitCount);

for (int i = 0; i < bitCount; i++)
{
    if (commonBits.IsSet(i))
    {
        gamma = gamma.SetBit(i);
    }
    else
    {
        epsilon = epsilon.SetBit(i);
    }
}

var powerConsumption = gamma * epsilon;

powerConsumption.ShouldBe(2261546);

WriteLine($"Power consumption : {powerConsumption}");

