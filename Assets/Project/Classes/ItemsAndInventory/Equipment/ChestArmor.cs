using System.Collections.Generic;
using Project.Classes.Characteristics;
using Project.Classes.ScriptableObjects;
using Project.Classes.Slots;

namespace Project.Classes.ItemsAndInventory.Equipment {
    using Type = Characteristic.Type;

    public class ChestArmor : Equipment {
        public ArmorCharacteristic Armor { get; protected set; }

        private ChestArmor(string name, Dictionary<Type, Characteristic> stats) : base(name, stats) {
            Armor = (ArmorCharacteristic) stats[Type.Armor];
        }
        
        public static ChestArmor CreateHeadArmor(ChestArmorSO chestArmorSO) {
            var stats = new Dictionary<Type, Characteristic>()
                {{Type.Armor, new ArmorCharacteristic(chestArmorSO.armor)}};
            return new ChestArmor(chestArmorSO.name, stats);
        }
        
        public override EquipmentSlot.Type RequiredSlot() {
            return EquipmentSlot.Type.Chest;
        }
    }
}