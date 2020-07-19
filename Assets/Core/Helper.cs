using System;

namespace Assets.Core
{
    internal static class Helper
    {
        public static int NextRandom(
        this Random random,
        int minValue,
        int maxValue)
        {
            return random.Next() * (maxValue - minValue) + minValue;
        }
    }
}
