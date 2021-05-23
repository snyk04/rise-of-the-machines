using Project.Classes.ItemsAndInventory.Equipment;
using UnityEngine;

namespace Project.Classes.Slots {
    public class WeaponSlot : EquipmentSlot {
        public enum Spot {
            LeftShoulder,
            RightShoulder,
            LeftHand,
            RightHand,
            TwoHands,
        }
        
        public Spot SlotSpot { get; private set; }

        public WeaponSlot(Spot slotSpot) {
            SlotSpot = slotSpot;
        }

        public override bool TryChangeItem(Equipment slotItem, out Equipment oldItem) {
            if (!(slotItem is Weapon)) {
                oldItem = null;
                return false;
            }
            
            RemoveCurrentItem(out oldItem);
            SlotItem = slotItem;
            Debug.Log($"{oldItem} changed to {slotItem}");
            return true;
        }
    }
}