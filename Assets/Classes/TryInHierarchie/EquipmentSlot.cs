﻿namespace Classes.TryInHierarchie {
    public abstract class EquipmentSlot {
        public Equipment SlotItem { get; protected set; }

        protected EquipmentSlot(Equipment slotItem) {
            SlotItem = slotItem;
        }

        public void RemoveCurrentItem(out Equipment item) {
            item = SlotItem;
            SlotItem = null;
        }

        public abstract bool TryChangeItem(Equipment slotItem);
    }
}