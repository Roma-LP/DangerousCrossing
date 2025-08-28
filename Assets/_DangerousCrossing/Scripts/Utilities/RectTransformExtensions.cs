using UnityEngine;

namespace _DangerousCrossing.Scripts.Utilities
{
    public static class RectTransformExtensions
    {
        private static void SetLeft(this RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        }

        private static void SetRight(this RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
        }

        private static void SetTop(this RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
        }

        private static void SetBottom(this RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }
        
        public static RectTransform Stretch(this RectTransform rt)
        {
            rt.anchorMax = Vector2.one;
            rt.anchorMin = Vector2.zero;
            rt.SetLeft(0);
            rt.SetRight(0);
            rt.SetTop(0);
            rt.SetBottom(0);

            return rt;
        }
    }
}