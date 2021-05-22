using UnityEngine;

namespace Project.Classes.ScriptableObjects {
    [CreateAssetMenu(fileName = "New Chest Armor", menuName = "ScriptableObject/ChestArmor", order = 0)]
    public class ChestArmorSO : ScriptableObject {
        [Header("Naming")]
        public string name;
        public int chestArmorID;

        [Header("Characteristics")]
        public float armor;
    }
}