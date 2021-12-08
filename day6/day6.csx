
var newBornFishes = new List<Fish>();

var fishes = File.ReadLines("day6/input.txt").First().Split(",").Select(int.Parse).Select(a => new Fish(a)).ToList();


for (int i = 0; i < 80; i++)
{
    var newBornes = new List<Fish>();
    foreach (var fish in fishes)
    {
        fish.Tick(newBornes);
    }
    fishes.AddRange(newBornes);
}



WriteLine("test");

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