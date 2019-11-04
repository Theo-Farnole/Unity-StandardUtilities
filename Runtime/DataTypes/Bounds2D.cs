namespace Utils
{
    using UnityEngine;

    [System.Serializable]
    public class Bounds2D
    {
        public float minimum;
        public float maximum;

        public Bounds2D(float min, float max)
        {
            minimum = min;
            maximum = max;
        }

        public Bounds2D(Vector2 limits)
        {
            minimum = limits.x;
            maximum = limits.y;
        }

        public bool WithinLimits(float value)
        {
            return (value >= minimum && value <= maximum);
        }

        public Vector2 AsVector2()
        {
            return new Vector2(minimum, maximum);
        }
    }
}