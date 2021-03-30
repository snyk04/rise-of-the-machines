using UnityEngine;

namespace Classes.TryInHierarchie {
    public class LegsSlot : EquipmentSlot {
        public LegsSlot(LegsArmor slotItem) : base(slotItem) { }

        public override bool TryChangeItem(Equipment slotItem) {
            if (!(slotItem is LegsArmor)) {
                return false;
            }

            SlotItem = slotItem;
            Debug.Log($"Item changed to {slotItem}");
            return true;
        }
    }
}