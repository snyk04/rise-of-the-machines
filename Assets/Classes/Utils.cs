using UnityEngine;

namespace Classes {
    public static class Vector2Extension {
        public static Vector2 RotateDegrees(this Vector2 vec2, float angle) {
            angle = angle.ToRadians();
            return RotateRadians(vec2, angle);
        }

        public static Vector2 RotateRadians(this Vector2 vec2, float angle) {
            return new Vector2 {
                x = vec2.x * Mathf.Cos(angle) + vec2.y * Mathf.Sin(angle),
                y = vec2.y * Mathf.Cos(angle) - vec2.x * Mathf.Sin(angle)
            };
        }
    }

    public static class AngleExtension {
        public static float ToRadians(this decimal number) {
            return (float) number * Mathf.PI / 180;
        }

        public static float ToRadians(this float number) {
            return number * Mathf.PI / 180;
        }

        public static float ToRadians(this int number) {
            return number * Mathf.PI / 180;
        }
    }
}