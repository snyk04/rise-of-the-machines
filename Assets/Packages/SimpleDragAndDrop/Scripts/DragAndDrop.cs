using UnityEngine;

namespace Packages.SimpleDragAndDrop.Scripts {
    public class DragAndDrop : MonoBehaviour
    {
        public enum Section
        {
            Armor,
            Weapons
        }

        public enum Slot
        {
            Everything,
            Head,
            Body,
            Legs,
            HandWeapon,
            ShoulderWeapon
        }
    }
}
