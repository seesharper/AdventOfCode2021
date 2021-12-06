public static bool IsSet(this int value, int bitPosition)
{
    return (value & (1 << bitPosition)) != 0;
}

public static int SetBit(this int value, int bitPosition)
{
    return value |= 1 << bitPosition;
}

public static int GetMostCommonBits(this int[] values, int bitCount)
{
    int[] bitsSet = new int[bitCount];
    foreach (var value in values)
    {
        for (int i = 0; i < bitCount; i++)
        {
            if (value.IsSet(i))
            {
                bitsSet[i]++;
            }
        }
    }

    int mostCommonBits = 0;

    for (int i = 0; i < bitCount; i++)
    {
        if (bitsSet[i] > values.Length)
        {
            mostCommonBits.SetBit(i);
        }
    }

    return mostCommonBits;
}