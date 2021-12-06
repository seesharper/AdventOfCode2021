#!/usr/bin/env dotnet-script


var depths = File.ReadLines("day1/input.txt").Select(int.Parse).ToArray();

int count = GetIncreasingMeasurements(depths);


WriteLine(count);


var startindex = 0;
List<int> groupsByThree = new List<int>();
while (startindex + 3 <= depths.Length)
{
    var depthsbyThree = depths[startindex..(startindex + 3)].Sum();
    groupsByThree.Add(depthsbyThree);
    startindex++; ;
}


WriteLine(GetIncreasingMeasurements(groupsByThree.ToArray()));


private int GetIncreasingMeasurements(int[] values)
{
    int count = 0;
    for (int i = 1; i < values.Length; i++)
    {
        if (values[i] > values[i - 1])
        {
            count++;
        }
    }

    return count;
}