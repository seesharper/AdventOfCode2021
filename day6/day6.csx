#load "../tools.csx"

var newBornFishes = new List<Fish>();

var input = File.ReadLines("day6/input.txt").First().Split(",").Select(int.Parse).Select(a => new Fish(a)).ToList();

var fishes = new List<Fish>(input);

for (int i = 0; i < 80; i++)
{
    var newBornes = new List<Fish>();
    foreach (var fish in fishes)
    {
        fish.Tick(newBornes);
    }
    fishes.AddRange(newBornes);
}

//fishes.Count().ShouldBe(352151);

WriteLine($"Number of fishes after 80 days: {fishes.Count}");

long[] typeMap = new long[9];
typeMap[0] = 0;
typeMap[1] = 0;
typeMap[2] = 0;
typeMap[3] = 0;
typeMap[4] = 0;
typeMap[5] = 0;
typeMap[6] = 0;
typeMap[7] = 0;
typeMap[8] = 0;
var types = File.ReadLines("day6/input.txt").First().Split(",").Select(int.Parse).GroupBy(k => k);
foreach (var type in types)
{
    typeMap[type.Key] = type.Count();
}

fishes = new List<Fish>(input);

for (int d = 0; d < 256; d++)
{
    long[] currentTypeMap = new long[9];
    Array.Copy(typeMap, currentTypeMap, 9);




    for (int i = 8; i >= 1; i--)
    {
        // if (i == 0)
        // {
        //     typeMap[6] += currentTypeMap[0];
        //     typeMap[8] += 1;
        // }
        // else
        {
            typeMap[i - 1] = currentTypeMap[i];
        }
    }

    typeMap[6] += currentTypeMap[0];
    var newBornCount = currentTypeMap[0] * 1;
    typeMap[8] = newBornCount;
    WriteLine("");

}

WriteLine($"Number of fishes after 80 days: {typeMap.Sum()}");




public record Fish()
{
    public Fish(int daysUntilSpawn) : this()
    {
        DaysUntilSpawn = daysUntilSpawn;
    }

    public int DaysUntilSpawn { get; private set; }

    public void Tick(List<Fish> newBornes)
    {
        if (DaysUntilSpawn == 0)
        {
            DaysUntilSpawn = 6;
            newBornes.Add(new Fish(8));
        }
        else
        {
            DaysUntilSpawn--;
        }

    }
}