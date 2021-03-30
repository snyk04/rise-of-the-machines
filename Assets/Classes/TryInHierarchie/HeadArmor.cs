namespace Classes.TryInHierarchie {
    public abstract class HeadArmor : Equipment {
        public ArmorCharacteristic Armor { get; protected set; }

        protected HeadArmor(string name, float armor) : base(name) {
            Armor = new ArmorCharacteristic(armor);
        }
    }
}