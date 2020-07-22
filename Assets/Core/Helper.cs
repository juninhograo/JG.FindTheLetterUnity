using System;
using UnityEngine;

namespace Assets.Core
{
    internal static class Helper
    {
        public static int NextRandom(
        this System.Random random,
        int minValue,
        int maxValue)
        {
            return random.Next() * (maxValue - minValue) + minValue;
        }

        public static float GetDrag(float aVelocityChange, float aFinalVelocity)
        {
            return aVelocityChange / ((aFinalVelocity + aVelocityChange) * Time.fixedDeltaTime);
        }
        public static float GetDragFromAcceleration(float aAcceleration, float aFinalVelocity)
        {
            return GetDrag(aAcceleration * Time.fixedDeltaTime, aFinalVelocity);
        }
    }
}
