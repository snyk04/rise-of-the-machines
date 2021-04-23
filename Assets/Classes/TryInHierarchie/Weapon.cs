using System.Collections.Generic;
using Classes.ScriptableObjects;

namespace Classes.TryInHierarchie
{
    using Type = Characteristic.Type;

    public class Weapon : Equipment
    {
        public enum ShotResult
        {
            NoAmmoInBackpack,
            NoAmmoInMagazine,
            ShotSuccesful,
            TooFast
        }

        public delegate void AmmoChange();
        public AmmoChange OnShot;
        public AmmoChange OnReload;

        public WeaponSO WeaponData { get; private set; }

        public static Weapon CreateWeapon(WeaponSO weaponSO)
        {
            var stats = new Dictionary<Type, Characteristic>() { { Type.Damage, new DamageCharacteristic(weaponSO.damage) } };
            return new Weapon(weaponSO, stats);
        }

        private Weapon(WeaponSO weaponSO, Dictionary<Type, Characteristic> stats) : base(weaponSO.name, stats)
        {
            WeaponData = weaponSO;
        }

        public float GetDamage() => Stats[Type.Damage].Value;

        public override EquipmentSlot.Type RequiredSlot()
        {
            return EquipmentSlot.Type.Weapon;
        }

        public ShotResult TryShoot(float timeAfterLastShot)
        {
            if (timeAfterLastShot < 1 / WeaponData.shotsPerSecond)
            {
                return ShotResult.TooFast;
            }
            if (WeaponData.currentBulletsInBackpack == 0)
            {
                return ShotResult.NoAmmoInBackpack;
            }
            if (WeaponData.currentBulletsInMagazine == 0)
            {
                return ShotResult.NoAmmoInMagazine;
            }

            WeaponData.currentBulletsInMagazine -= 1;
            WeaponData.currentBulletsInBackpack -= 1;
            OnShot?.Invoke();

            return ShotResult.ShotSuccesful;
        }
        public void Reload()
        {
            while ((WeaponData.currentBulletsInMagazine < WeaponData.maxBulletsInMagazine) && (WeaponData.currentBulletsInMagazine < WeaponData.currentBulletsInBackpack))
            {
                WeaponData.currentBulletsInMagazine += 1;
            }
            OnReload?.Invoke();
        }
    }
}
