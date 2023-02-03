using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lapis.Extension
{
    public static partial class Extension
    {
        public static void SetRotationX(this Transform transform, float amount)
            => SetRotation(transform, 'X', amount);

        public static void SetRotationY(this Transform transform, float amount)
            => SetRotation(transform, 'Y', amount);

        public static void SetRotationZ(this Transform transform, float amount)
            => SetRotation(transform, 'Z', amount);

        private static void SetRotation(this Transform transform, char axis, float amount)
        {
            switch (axis)
            {
                case 'X':
                    transform.Rotate(amount, 0, 0);
                    break;
                case 'Y':
                    transform.Rotate(0, amount, 0);
                    break;
                case 'Z':
                    transform.Rotate(0, 0, amount);
                    break;
            }
        }
    }
}

