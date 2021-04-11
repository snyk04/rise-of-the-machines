﻿using System.Collections.Generic;
using Classes.ScriptableObjects;

namespace Classes.TryInHierarchie {
    using Type = Classes.TryInHierarchie.Characteristic.Type;

    public class Weapon : Equipment {
        public WeaponSO WeaponData { get; private set; }

        public static Weapon CreateWeapon(WeaponSO weaponSO, float damage) {
            var stats = new Dictionary<Type, Characteristic>() {{Type.Damage, new DamageCharacteristic(damage)}};
            return new Weapon(weaponSO, stats);
        }

        public Weapon(WeaponSO weaponSO, Dictionary<Type, Characteristic> stats) : base(weaponSO.name, stats) {
            WeaponData = weaponSO;
        }
        
        public float GetDamage() => Stats[Type.Damage].Value;

        public override EquipmentSlot.Type RequiredSlot() {
            return EquipmentSlot.Type.Weapon;
        }
    }
}