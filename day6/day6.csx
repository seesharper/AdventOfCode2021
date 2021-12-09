#load "../tools.csx"

long[] originalTypeMap = new long[9];

var types = File.ReadLines("day6/input.txt").First().Split(",").Select(int.Parse).GroupBy(k => k);
foreach (var type in types)
{
    originalTypeMap[type.Key] = type.Count();
}

var numberOfFishesAfter80Days = GetFishCount(numberOfDays: 80);
numberOfFishesAfter80Days.ShouldBe(352151);
WriteLine($"Number of fishes after 80 days: {numberOfFishesAfter80Days}");


var numberOfFishesAfter256Days = GetFishCount(numberOfDays: 256);
numberOfFishesAfter256Days.ShouldBe(1601616884019);
WriteLine($"Number of fishes after 256 days: {numberOfFishesAfter256Days}");

private long GetFishCount(int numberOfDays)
{
    long[] typeMap = new long[9];
    Array.Copy(originalTypeMap, typeMap, 9);

    for (int d = 0; d < numberOfDays; d++)
    {
        long[] currentTypeMap = new long[9];
        Array.Copy(typeMap, currentTypeMap, 9);

        for (int i = 8; i >= 1; i--)
        {
            typeMap[i - 1] = currentTypeMap[i];
        }

        typeMap[6] += currentTypeMap[0];
        var newBornCount = currentTypeMap[0] * 1;
        typeMap[8] = newBornCount;
    }

    return typeMap.Sum();
}