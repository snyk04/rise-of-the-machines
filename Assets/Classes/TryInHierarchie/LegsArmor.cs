using System.Collections.Generic;
using Classes.ScriptableObjects;

namespace Classes.TryInHierarchie {
    using Type = Classes.TryInHierarchie.Characteristic.Type;

    public class LegsArmor : Equipment {
        public ArmorCharacteristic Armor { get; private set; }

        private LegsArmor(string name, Dictionary<Type, Characteristic> stats) : base(name, stats) {
            Armor = (ArmorCharacteristic) stats[Type.Armor];
        }

        public static LegsArmor CreateLegsArmor(LegsArmorSO legsArmorSO) {
            var stats = new Dictionary<Type, Characteristic>()
                {{Type.Armor, new ArmorCharacteristic(legsArmorSO.armor)}};
            return new LegsArmor(legsArmorSO.name, stats);
        }

        public override EquipmentSlot.Type RequiredSlot() {
            return EquipmentSlot.Type.Legs;
        }
    }
}