namespace Lortedo.Utilities.DataTypes
{
    using UnityEngine;

    [System.Serializable]
    public class Bounds2D
    {
        public float min;
        public float max;

        public Bounds2D(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public Bounds2D(Vector2 limits)
        {
            min = limits.x;
            max = limits.y;
        }

        public bool WithinLimits(float value)
        {
            return (value >= min && value <= max);
        }
    }

    public static class Bounds2DExtension
    {
        public static Vector2 ToVector(this Bounds2D b)
        {
            return new Vector2(b.min, b.max);
        }

        public static bool WithinLimits(this Bounds2D b, float value)
        {
            return (value >= b.min && value <= b.max);
        }

        public static float Clamp(this Bounds2D b, float value)
        {
            return Mathf.Clamp(value, b.min, b.max);
        }
    }
}