namespace Classes.TryInHierarchie {
    public abstract class LegsArmor : Equipment {
        public ArmorCharacteristic Armor { get; protected set; }

        protected LegsArmor(string name, float armor) : base(name) {
            Armor = new ArmorCharacteristic(armor);
        }
    }
}