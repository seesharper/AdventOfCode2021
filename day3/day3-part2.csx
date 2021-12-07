#load "../tools.csx"

var input = File.ReadLines("day3/input.txt").ToArray().Select(e => Convert.ToInt32(e, 2)).ToArray();

int bitCount = 12;
int oxygenGeneratorRating = 0;
int CO2ScrubberRating = 0;

var values = input.ToArray();

for (int i = 0; i < bitCount; i++)
{
    var mostCommonBits = values.GetMostCommonBits(bitCount, 1);
    if (mostCommonBits.IsSet(bitCount - i - 1))
    {
        values = values.Where(v => v.IsSet(bitCount - i - 1)).ToArray();
    }
    else
    {
        values = values.Where(v => !v.IsSet(bitCount - i - 1)).ToArray();
    }
    if (values.Length == 1)
    {
        oxygenGeneratorRating = values[0];
        break;
    }
}

WriteLine($"Oxygen Generator Rating : {oxygenGeneratorRating}");

values = input.ToArray();
for (int i = 0; i < bitCount; i++)
{
    var mostCommonBits = values.GetMostCommonBits(bitCount, 1);
    if (mostCommonBits.IsSet(bitCount - i - 1))
    {
        values = values.Where(v => !v.IsSet(bitCount - i - 1)).ToArray();
    }
    else
    {
        values = values.Where(v => v.IsSet(bitCount - i - 1)).ToArray();
    }
    if (values.Length == 1)
    {
        CO2ScrubberRating = values[0];
        break;
    }
}

WriteLine($"CO2 Scrubber Rating : {CO2ScrubberRating}");

var lifeSupportRating = oxygenGeneratorRating * CO2ScrubberRating;


lifeSupportRating.ShouldBe(6775520);
WriteLine($"Life support rating : {lifeSupportRating}");

Console.WriteLine("");