using UnityEngine;

namespace Classes.TryInHierarchie {
    public class ChestSlot : EquipmentSlot {
        public ChestSlot(ChestArmor slotItem) : base(slotItem) { }

        public override bool TryChangeItem(Equipment slotItem) {
            if (!(slotItem is ChestArmor)) {
                return false;
            }

            SlotItem = slotItem;
            Debug.Log($"Item changed to {slotItem}");
            return true;
        }
    }
}