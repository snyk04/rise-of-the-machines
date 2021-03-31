using UnityEngine;

namespace Classes.TryInHierarchie {
    public class LegsSlot : EquipmentSlot {
        public LegsSlot() { }
        public LegsSlot(LegsArmor slotItem) : base(slotItem) { }

        public override bool TryChangeItem(Equipment slotItem, out Equipment oldItem) {
            if (!(slotItem is LegsArmor)) {
                oldItem = null;
                return false;
            }
            
            RemoveCurrentItem(out oldItem);
            SlotItem = slotItem;
            Debug.Log($"Item changed to {slotItem}");
            return true;
        }
    }
}