using UnityEngine;

namespace Extensions
{
    public static class Colors
    {
        public static readonly Color white = Color(255,255,255);
        public static readonly Color black = Color(0, 0, 0);

        public static readonly Color darkRed = Color(200, 0, 0);
        public static readonly Color darkBlue = Color(15, 0, 255);

        public static readonly Color lightRed = Color(255, 50, 50);
        public static readonly Color lightBlue = Color(50, 240, 255);
        public static readonly Color lightYellow = Color(255, 255, 180);
        public static readonly Color lightGreen = Color(160, 240, 100);
        public static readonly Color lightGreenBlue = Color(0, 255, 175);

        public static readonly Color orange = Color(220, 100, 30);
        public static readonly Color blue = Color(20, 215, 150);
        public static readonly Color purple = Color(180, 75, 230);

        public static Color Color(int r, int g, int b)
        {
            return new Color(r / 255f, g / 255f, b / 255f);
        }

        public static Color Color(int r, int g, int b, int a)
        {
            return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }

        public static Color ColorWithModifiedAlpha(Color color, float aplha)
        {
            return new Color(color.r, color.g, color.b, aplha);
        }
    }
}
