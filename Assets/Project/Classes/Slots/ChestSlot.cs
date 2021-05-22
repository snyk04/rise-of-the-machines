using Project.Classes.CanBePickedUp.Equipment;
using UnityEngine;

namespace Project.Classes.Slots {
    public class ChestSlot : EquipmentSlot {
        public ChestSlot() { }
        public ChestSlot(ChestArmor slotItem) : base(slotItem) { }

        public override bool TryChangeItem(Equipment slotItem, out Equipment oldItem) {
            if (!(slotItem is ChestArmor)) {
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