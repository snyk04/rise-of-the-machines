namespace Classes.TryInHierarchie {
    public abstract class ChestArmor : Equipment {
        public ArmorCharacteristic Armor { get; protected set; }

        protected ChestArmor(string name, Health health, ArmorCharacteristic armor) : base(name, health) {
            Armor = armor;
        }
    }
}