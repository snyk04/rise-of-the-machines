using System;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

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

        public static float ToRadians(this float angle) {
            return angle * Mathf.PI / 180;
        }

        public static float ToRadians(this int angle) {
            return angle * Mathf.PI / 180;
        }

        public static double ToRadians(this double angle) {
            return angle * Mathf.PI / 180;
        }

        public static float NormalizeAngle(this float angle) {
            return angle >= 0 ? angle % 360 : (360 + angle % 360).NormalizeAngle();
        }

        public static float NormalizeAngle180(this float angle) {
            var remainder  = angle % 360;
            return remainder  < 180 ? remainder  : remainder  - 360;
        }
    }
}