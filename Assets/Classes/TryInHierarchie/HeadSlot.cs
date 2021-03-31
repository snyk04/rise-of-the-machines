using UnityEngine;

namespace Classes.TryInHierarchie {
    public class HeadSlot : EquipmentSlot {
        public HeadSlot() { }
        public HeadSlot(HeadArmor slotItem) : base(slotItem) { }

        public override bool TryChangeItem(Equipment slotItem, out Equipment oldItem) {
            if (!(slotItem is HeadArmor)) {
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