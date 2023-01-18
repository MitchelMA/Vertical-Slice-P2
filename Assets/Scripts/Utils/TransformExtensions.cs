using UnityEngine;

namespace Utils
{
    public static class TransformExtensions
    {
        public static bool Translate(this Transform transform, Vector3 translation, Vector4 bounds)
        {
            Vector3 nextPos = transform.position + translation;
            if (!nextPos.IsInBounds(bounds))
                return false;

            transform.Translate(translation);
            return true;
        }
    }
}