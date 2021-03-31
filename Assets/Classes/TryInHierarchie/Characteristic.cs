namespace Classes.TryInHierarchie {
    public abstract class Characteristic {
        public enum Type {
            Health,
            Speed,
            Armor
        }
        public float Value { get; protected set; }
        protected Characteristic(float value) {
            Value = value;
        }
    }
}