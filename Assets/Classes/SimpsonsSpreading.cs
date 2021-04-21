using Classes.ScriptableObjects;
using UnityEngine;

namespace Classes
{
    public class SimpsonsSpreading
    {
        private static float FindG(float random, float a)
        {
            return Mathf.Sqrt(2 * random) * a - a;
        }
        private static float FindRandNumberUsingSimpson(float random, float a)
        {
            return random <= 0.5 ? FindG(random, a) : -FindG(1 - random, a);
        }
        public static Vector3 Spreading(float a)
        {
            return new Vector3(FindRandNumberUsingSimpson(Random.value, a), 0, WeaponSO.DEFAULT_SPREAD_DISTANCE).normalized;
        }
    }
}
