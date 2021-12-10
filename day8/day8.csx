#load "../tools.csx"

public record Entry(string[] Signals, string[] Output);

var entries = File.ReadAllLines("day8/input.txt").Select(l => new Entry(l[..l.IndexOf("|")].Trim().Split(" "), l[(l.IndexOf("|") + 1)..].Trim().Split(" "))).ToArray();

int[] uniqueSegmentLengths = new int[4] { 2, 3, 4, 7 };

var numberOfDigitsWithUniqueSegmentLengths = entries.SelectMany(e => e.Output).Count(s => uniqueSegmentLengths.Contains(s.Length));
numberOfDigitsWithUniqueSegmentLengths.ShouldBe(504);
WriteLine($"Number of digits with unique segment lengths : {numberOfDigitsWithUniqueSegmentLengths}");


int sum = 0;
foreach (var entry in entries)
{
    var mappings = GetMapping2(entry);
    sum += Decode(entry.Output, mappings);
}

sum.ShouldBe(1073431);

WriteLine($"The sum of all numbers is : {sum}");


public int Decode(string[] output, Dictionary<string, int> mappings)
{
    int number = 0;
    for (int i = 0; i < 4; i++)
    {
        var digit = mappings[new string(output[i].OrderBy(c => c).ToArray())];
        number += digit * (int)Math.Pow(10, 3 - i);
    }

    return number;
}

public Dictionary<string, int> GetMapping2(Entry entry)
{
    var mappings = new Dictionary<string, int>();

    var segmentsFor1 = entry.Signals.Single(s => s.Length == 2);
    var segmentsFor4 = entry.Signals.Single(s => s.Length == 4);
    var segmentsFor7 = entry.Signals.Single(s => s.Length == 3);
    var segmentsFor8 = entry.Signals.Single(s => s.Length == 7);
    var segmentsFor3 = entry.Signals.Single(s => s.Length == 5 && s.ContainsLetters(segmentsFor1));
    var segmentsFor9 = entry.Signals.Single(s => s.Length == 6 && s.ContainsLetters(segmentsFor4));
    var segmentsFor6 = entry.Signals.Single(s => s.Length == 6 && !s.ContainsLetters(segmentsFor1));
    var segmentsFor0 = entry.Signals.Single(s => s.Length == 6 && !new[] { segmentsFor9, segmentsFor6 }.Contains(s));

    var segmentC = segmentsFor8.Single(s => !segmentsFor6.Contains(s));

    var segmentsFor2 = entry.Signals.Single(s => s.Length == 5 && s.Contains(segmentC) && s != segmentsFor3);
    var segmentsFor5 = entry.Signals.Single(s => s.Length == 5 && !s.Contains(segmentC) && s != segmentsFor3);

    mappings[new string(segmentsFor0.OrderBy(c => c).ToArray())] = 0;
    mappings[new string(segmentsFor1.OrderBy(c => c).ToArray())] = 1;
    mappings[new string(segmentsFor2.OrderBy(c => c).ToArray())] = 2;
    mappings[new string(segmentsFor3.OrderBy(c => c).ToArray())] = 3;
    mappings[new string(segmentsFor4.OrderBy(c => c).ToArray())] = 4;
    mappings[new string(segmentsFor5.OrderBy(c => c).ToArray())] = 5;
    mappings[new string(segmentsFor6.OrderBy(c => c).ToArray())] = 6;
    mappings[new string(segmentsFor7.OrderBy(c => c).ToArray())] = 7;
    mappings[new string(segmentsFor8.OrderBy(c => c).ToArray())] = 8;
    mappings[new string(segmentsFor9.OrderBy(c => c).ToArray())] = 9;

    return mappings;
}

public static bool ContainsLetters(this string value, string letters)
{
    bool result = value.Intersect(letters).Count() == letters.Length;
    return result;
}
