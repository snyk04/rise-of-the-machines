using UnityEngine;

namespace Classes.TryInHierarchie {
    public class WeaponSlot : EquipmentSlot {
        public WeaponSlot() { }
        public WeaponSlot(Weapon slotItem) : base(slotItem) { }

        public override bool TryChangeItem(Equipment slotItem, out Equipment oldItem) {
            if (!(slotItem is Weapon)) {
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