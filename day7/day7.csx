#load "../tools.csx"

var positions = File.ReadLines("day7/input.txt").First().Split(",").Select(int.Parse).ToArray(); ;

var fuelsWithContantsConsumptionRate = GetLowestFuels(constantConsumptionRate: true);
fuelsWithContantsConsumptionRate.ShouldBe(359648);
WriteLine($"Lowest fuels with linear fuel consumption: {fuelsWithContantsConsumptionRate}");

var fuelsWithGrowingConsumptionRate = GetLowestFuels(constantConsumptionRate: false);
fuelsWithGrowingConsumptionRate.ShouldBe(100727924);
WriteLine($"Lowest fuels with non-linear fuel consumption: {fuelsWithGrowingConsumptionRate}");

private int GetLowestFuels(bool constantConsumptionRate)
{
    int lowestFuels = int.MaxValue;

    for (int f = 0; f < positions.Length; f++)
    {
        int fuels = 0;
        for (int i = 0; i < positions.Length; i++)
        {
            var distance = Math.Abs(positions[i] - f);
            if (constantConsumptionRate)
            {
                fuels += distance;
            }
            else
            {
                fuels += distance * (distance + 1) / 2;
            }
        }
        if (fuels < lowestFuels)
        {
            lowestFuels = fuels;
        }
    }

    return lowestFuels;
}