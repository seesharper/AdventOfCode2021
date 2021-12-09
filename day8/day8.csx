#load "../tools.csx"

var entries = File.ReadAllLines("day8/input.txt").Select(l => new Entry(l[..l.IndexOf("|")].Trim().Split(" "), l[(l.IndexOf("|") + 1)..].Trim().Split(" "))).ToArray();

int[] uniqueSegmentLengths = new int[4] { 2, 3, 4, 7 };

var numberOfDigitsWithUniqueSegmentLengths = entries.SelectMany(e => e.Output).Count(s => uniqueSegmentLengths.Contains(s.Length));
numberOfDigitsWithUniqueSegmentLengths.ShouldBe(504);
WriteLine($"Number of digits with unique segment lengths : {numberOfDigitsWithUniqueSegmentLengths}");

public record Entry(string[] Signals, string[] Output);
