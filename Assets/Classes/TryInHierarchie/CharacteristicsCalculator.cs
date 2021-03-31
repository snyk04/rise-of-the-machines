using System.Collections.Generic;
using System.Linq;
using Type = Classes.TryInHierarchie.Characteristic.Type;

namespace Classes.TryInHierarchie {
    public static class CharacteristicsCalculator {
        private static Dictionary<Type, float> result = new Dictionary<Type, float> {
            {Type.Health, 0},
            {Type.Armor, 0},
            {Type.Speed, 0},
        };

        public static Dictionary<Type, float> Calculate(params Characteristic[] characteristics) {
            var healthValue = characteristics.OfType<Health>().Sum(elem => elem.Value);
            var armorValue = characteristics.OfType<ArmorCharacteristic>().Sum(elem => elem.Value);
            var speedValue = characteristics.OfType<SpeedCharacteristic>().Sum(elem => elem.Value);
            result[Type.Health] = healthValue;
            result[Type.Armor] = armorValue;
            result[Type.Speed] = speedValue;
            return result;
        }
    }
}