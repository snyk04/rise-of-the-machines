using UnityEngine;

namespace Classes.ScriptableObjects {
    [CreateAssetMenu(fileName = "New Head Armor", menuName = "ScriptableObject/HeadArmor", order = 0)]
    public class HeadArmorSO : ScriptableObject {
        [Header("Naming")]
        public string name;
        public int headArmorID;

        [Header("Characteristics")]
        public float armor;
    }
}