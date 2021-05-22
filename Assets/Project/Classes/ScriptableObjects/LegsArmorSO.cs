using UnityEngine;

namespace Project.Classes.ScriptableObjects {
    [CreateAssetMenu(fileName = "New Legs Armor", menuName = "ScriptableObject/LegsArmor", order = 0)]
    public class LegsArmorSO : ScriptableObject {
        [Header("Naming")]
        public string name;
        public int legsArmorID;

        [Header("Characteristics")]
        public float armor;
    }
}