using Project.Classes.CanBePickedUp.Equipment;

namespace Project.Classes.Slots {
    public abstract class EquipmentSlot {
        public enum Type {
            Head,
            Chest,
            Legs,
            Weapon
        }

        public Equipment SlotItem { get; protected set; }

        protected EquipmentSlot() { }

        protected EquipmentSlot(Equipment slotItem) {
            SlotItem = slotItem;
        }

        public void RemoveCurrentItem(out Equipment item) {
            item = SlotItem;
            SlotItem = null;
        }

        public abstract bool TryChangeItem(Equipment slotItem, out Equipment oldItem);
    }
}