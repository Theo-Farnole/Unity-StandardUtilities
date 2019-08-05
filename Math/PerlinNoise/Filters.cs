using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lortedo.Filters
{
    [System.Serializable]
    public class ClampFilter
    {
        [SerializeField] private bool _enableClamping = false;
        [Space]
        [SerializeField, Range(0, 1)] private float _maxPercent = 0.2f;
        [Space]
        [SerializeField] private float _height = -2;

        public float ApplyFilter(float val)
        {
            if (!_enableClamping)
                return val;

            bool isWater = val <= _maxPercent;

            if (isWater)
            {
                val = _height;
            }

            return val;
        }
    }

    [System.Serializable]
    public class RandomSurfaceFilter
    {
        [SerializeField] private bool _enableSurfaceFilter = true;
        [Space]
        [SerializeField] private float _heightMaxGap = 0.3f;

        private System.Random _random = new System.Random();

        public float ApplyFilter(float val)
        {
            if (!_enableSurfaceFilter)
                return val;

            float rand = (float)_random.NextDouble(); // 0.0f to 1.0f
            rand *= 2; // 0.f to 2f
            rand -= 1f; // -1f to 1f;

            val += _heightMaxGap * rand;

            return val;
        }
    }

    [System.Serializable]
    public class IslandFilter
    {
        [SerializeField] private bool _enableIslandEffect = false;
        [Space]
        [SerializeField] private float _islandRadius = 20;
        [SerializeField, Range(0, 3)] private float _islandMultipler = 1;

        public float ApplyFilter(float val, Vector3 position, Vector3 center)
        {
            if (!_enableIslandEffect)
                return val;

            float distFromCenter = Vector3.Distance(position, center);
            float y = 1 - (distFromCenter / _islandRadius);

            return val + y * _islandMultipler;
        }
    }
}