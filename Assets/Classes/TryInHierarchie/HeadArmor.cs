using System.Collections.Generic;

namespace Classes.TryInHierarchie {
    using Type = Classes.TryInHierarchie.Characteristic.Type;

    public abstract class HeadArmor : Equipment {
        public ArmorCharacteristic Armor { get; protected set; }

        protected HeadArmor(string name, float armor, Dictionary<Type, Characteristic> stats) : base(name, stats) {
            Armor = new ArmorCharacteristic(armor);
        }
        public override EquipmentSlot.Type RequiredSlot() {
            return EquipmentSlot.Type.Head;
        }
    }
}