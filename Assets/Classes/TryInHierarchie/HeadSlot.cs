using UnityEngine;

namespace Classes.TryInHierarchie {
    public class HeadSlot : EquipmentSlot {
        public HeadSlot(HeadArmor slotItem) : base(slotItem) { }

        public override bool TryChangeItem(Equipment slotItem) {
            if (!(slotItem is HeadArmor)) {
                return false;
            }

            SlotItem = slotItem;
            Debug.Log($"Item changed to {slotItem}");
            return true;
        }
    }
}