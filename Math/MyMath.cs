using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class Math
    {
        /// <summary>
        /// If int is greater than max, so int=max
        /// Else, if int is minor than min, so int=max 
        /// </summary>
        public static int InverseClamp(int i, int min, int max)
        {
            int value = i;

            if (value > max)
            {
                value = min;
            }

            if (value < min)
            {
                value = max;
            }

            return value;
        }

        /// <summary>
        /// Return an Vector2 from an angle in radians.
        /// </summary>
        /// <param name="angle">In radians</param>
        /// <returns></returns>
        public static Vector2 AngleToVector2(float angle)
        {
            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
    }
}
