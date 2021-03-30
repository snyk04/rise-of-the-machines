namespace Classes.TryInHierarchie {
    public abstract class HeadArmor : Equipment {
        public ArmorCharacteristic Armor { get; protected set; }

        protected HeadArmor(string name, Health health, ArmorCharacteristic armor) : base(name, health) {
            Armor = armor;
        }
    }
}