/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

namespace Paroxe.PdfRenderer.Internal
{
    public static class PDFInternalUtils
    {
        public static float CalculateRectTransformIntersectArea(RectTransform a, RectTransform b)
        {
            Vector3[] worldCorners = new Vector3[4];

            a.GetWorldCorners(worldCorners);
            Vector2 min = worldCorners[0];
            Vector2 max = worldCorners[0];

            for (int i = 1; i < 4; ++i)
            {
                if (worldCorners[i].x < min.x)
                    min.x = worldCorners[i].x;
                if (worldCorners[i].y < min.y)
                    min.y = worldCorners[i].y;
                if (worldCorners[i].x > max.x)
                    max.x = worldCorners[i].x;
                if (worldCorners[i].y > max.y)
                    max.y = worldCorners[i].y;
            }

            Rect ra = new Rect(min.x, min.y, max.x - min.x, max.y - min.y);

            b.GetWorldCorners(worldCorners);

            min = worldCorners[0];
            max = worldCorners[0];

            for (int i = 1; i < 4; ++i)
            {
                if (worldCorners[i].x < min.x)
                    min.x = worldCorners[i].x;
                if (worldCorners[i].y < min.y)
                    min.y = worldCorners[i].y;
                if (worldCorners[i].x > max.x)
                    max.x = worldCorners[i].x;
                if (worldCorners[i].y > max.y)
                    max.y = worldCorners[i].y;
            }

            Rect rb = new Rect(min.x, min.y, max.x - min.x, max.y - min.y);

            float x_overlap = Mathf.Min(ra.xMin + ra.width, rb.xMin + rb.width) - Mathf.Max(ra.xMin, rb.xMin) + 1;
            float y_overlap = Mathf.Min(ra.yMin + ra.height, rb.yMin + rb.height) - Mathf.Max(ra.yMin, rb.yMin) + 1;

            if (x_overlap <= 0.0f || y_overlap <= 0.0f)
                return 0.0f;

            return x_overlap*y_overlap;
        }

        public static float CalculateRectTransformVerticalDistance(RectTransform a, RectTransform b)
        {
            if (CalculateRectTransformIntersectArea(a, b) > 0.0f)
                return 0.0f;

            Vector3[] worldCorners = new Vector3[4];

            a.GetWorldCorners(worldCorners);
            Vector2 min = worldCorners[0];
            Vector2 max = worldCorners[0];

            for (int i = 1; i < 4; ++i)
            {
                if (worldCorners[i].x < min.x)
                    min.x = worldCorners[i].x;
                if (worldCorners[i].y < min.y)
                    min.y = worldCorners[i].y;
                if (worldCorners[i].x > max.x)
                    max.x = worldCorners[i].x;
                if (worldCorners[i].y > max.y)
                    max.y = worldCorners[i].y;
            }

            Rect ra = new Rect(min.x, min.y, max.x - min.x, max.y - min.y);

            b.GetWorldCorners(worldCorners);

            min = worldCorners[0];
            max = worldCorners[0];

            for (int i = 1; i < 4; ++i)
            {
                if (worldCorners[i].x < min.x)
                    min.x = worldCorners[i].x;
                if (worldCorners[i].y < min.y)
                    min.y = worldCorners[i].y;
                if (worldCorners[i].x > max.x)
                    max.x = worldCorners[i].x;
                if (worldCorners[i].y > max.y)
                    max.y = worldCorners[i].y;
            }

            Rect rb = new Rect(min.x, min.y, max.x - min.x, max.y - min.y);

            return Mathf.Min(rb.yMin - ra.yMax, rb.yMax - ra.yMin);
        }

        public static float CubicEaseIn(float currentTime, float startingValue, float finalValue, float duration)
        {
            return finalValue*(currentTime /= duration)*currentTime*currentTime + startingValue;
        }
    }
}
