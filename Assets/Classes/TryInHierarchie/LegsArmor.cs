using System.Collections.Generic;

namespace Classes.TryInHierarchie {
    public abstract class LegsArmor : Equipment {
        public ArmorCharacteristic Armor { get; protected set; }

        protected LegsArmor(string name, float armor, Dictionary<Characteristic.Type, Characteristic> stats) : base(name, stats) {
            Armor = new ArmorCharacteristic(armor);
        }
        public override EquipmentSlot.Type RequiredSlot() {
           return EquipmentSlot.Type.Legs;
        }
    }
}