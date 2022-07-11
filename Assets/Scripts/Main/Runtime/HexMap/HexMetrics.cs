using UnityEngine;

namespace MetaArea
{
    public static class HexMetrics
    {
        public const float outerRadius = 82f;

        public const float innerRadius = outerRadius * 0.866025404f;

        public const float BackLine = 17;

        public const float outerBackLineRadius = outerRadius - BackLine;

        public const float innerBackLineRadius = outerBackLineRadius * 0.866025404f;

        public static Vector3[] corners =
        {
            new Vector3(0f, 0f, outerBackLineRadius),
            new Vector3(innerBackLineRadius, 0f, 0.5f * outerBackLineRadius),
            new Vector3(innerBackLineRadius, 0f, -0.5f * outerBackLineRadius),
            new Vector3(0f, 0f, -outerBackLineRadius),
            new Vector3(-innerBackLineRadius, 0f, -0.5f * outerBackLineRadius),
            new Vector3(-innerBackLineRadius, 0f, 0.5f * outerBackLineRadius),
            new Vector3(0f, 0f, outerBackLineRadius)
        };
    }
}