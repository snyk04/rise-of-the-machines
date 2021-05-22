namespace Classes.TryInHierarchie {
    public class ArmorCharacteristic : Characteristic {

        private float armorCoefficient;

        public ArmorCharacteristic(float armor, float armorCoefficient = 0.05f) : base(armor) {
            this.armorCoefficient = armorCoefficient;
        }

        public float GetResistance() {
            return 1 / (1 + armorCoefficient * Value);
        }
    }
}