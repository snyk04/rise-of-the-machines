using System.Collections.Generic;
using Project.Classes.Characteristics;
using Project.Classes.ScriptableObjects;
using Project.Classes.Slots;

namespace Project.Classes.CanBePickedUp.Equipment {
    using Type = Characteristic.Type;

    public class HeadArmor : Equipment {
        public ArmorCharacteristic Armor { get; protected set; }

        private HeadArmor(string name, Dictionary<Type, Characteristic> stats) : base(name, stats) {
            Armor = (ArmorCharacteristic) stats[Type.Armor];
        }
        
        public static HeadArmor CreateHeadArmor(HeadArmorSO headArmorSO) {
            var stats = new Dictionary<Type, Characteristic>()
                {{Type.Armor, new ArmorCharacteristic(headArmorSO.armor)}};
            return new HeadArmor(headArmorSO.name, stats);
        }
        
        public override EquipmentSlot.Type RequiredSlot() {
            return EquipmentSlot.Type.Head;
        }
    }
}