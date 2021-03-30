namespace Classes.TryInHierarchie {
    public abstract class LegsArmor : Equipment {
        public ArmorCharacteristic Armor { get; protected set; }

        protected LegsArmor(string name, Health health, ArmorCharacteristic armor) : base(name, health) {
            Armor = armor;
        }
    }
}