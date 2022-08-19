using UnityEngine;

namespace Hexed.Wrappers
{
    internal class UnityWrappers
    {
        public static bool IsBadPosition(Vector3 v3)
        {
            if (IsInfinity(v3)) return true;
            if (IsNaN(v3)) return true;
            return false;
        }

        public static bool IsBadRotation(Quaternion v3)
        {
            if (IsInfinityRotation(v3)) return true;
            if (IsNaNRotation(v3)) return true;
            return false;
        }

        private static bool IsNaN(Vector3 v3)
        {
            return float.IsNaN(v3.x) || float.IsNaN(v3.y) || float.IsNaN(v3.z) || float.IsInfinity(v3.x) || float.IsInfinity(v3.y) || float.IsInfinity(v3.z);
        }

        private static bool IsInfinity(Vector3 v3)
        {
            return 998001 <= v3.x || 998001 <= v3.y || 998001 <= v3.z || -998001 >= v3.x || -998001 >= v3.y || -998001 >= v3.z;
        }

        private static bool IsNaNRotation(Quaternion v3)
        {
            return float.IsNaN(v3.x) || float.IsNaN(v3.y) || float.IsNaN(v3.z) || float.IsInfinity(v3.x) || float.IsInfinity(v3.y) || float.IsInfinity(v3.z);
        }

        private static bool IsInfinityRotation(Quaternion v3)
        {
            return 998001 <= v3.x || 998001 <= v3.y || 998001 <= v3.z || 998001 <= v3.w || -998001 >= v3.x || -998001 >= v3.y || -998001 >= v3.z || -998001 >= v3.w;
        }
    }
}
