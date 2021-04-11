using UnityEngine;

namespace Classes.TryInHierarchie {
    public class WeaponSlot : EquipmentSlot {
        public enum Type {
            LeftShoulder,
            RightShoulder,
            LeftHand,
            RightHand,
            TwoHands,
        }
        
        public Type SlotType { get; private set; }

        public WeaponSlot(Type slotType) {
            SlotType = slotType;
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