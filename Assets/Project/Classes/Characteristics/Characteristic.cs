namespace Project.Classes.Characteristics {
    public abstract class Characteristic {
        public enum Type {
            Health,
            Speed,
            Armor,
            Damage
        }
        public float Value { get; set; }
        protected Characteristic(float value) {
            Value = value;
        }
    }
}