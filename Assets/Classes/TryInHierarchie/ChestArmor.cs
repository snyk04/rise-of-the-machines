namespace Classes.TryInHierarchie {
    public abstract class ChestArmor : Equipment {
        public ArmorCharacteristic Armor { get; protected set; }

        protected ChestArmor(string name, float armor) : base(name) {
            Armor = new ArmorCharacteristic(armor);
        }
    }
}