using System.Collections;
using System.Collections.Generic;
using Classes.ScriptableObjects;
using UnityEngine;

namespace Classes.TryInHierarchie {
    using Type = Characteristic.Type;

    public class Weapon : Equipment {
        public enum ShotResult {
            NoAmmoInBackpack,
            NoAmmoInMagazine,
            ShotSuccessful,
            TooFast
        }

        public delegate void AmmoChange();

        public AmmoChange OnShot;
        public AmmoChange OnReloadStart;
        public AmmoChange OnReloadEnd;
        public float LastShotTime { get; private set; }

        public WeaponSO WeaponData { get; private set; }

        public static Weapon CreateWeapon(WeaponSO weaponSO) {
            var stats = new Dictionary<Type, Characteristic>()
                {{Type.Damage, new DamageCharacteristic(weaponSO.damage)}};
            return new Weapon(weaponSO, stats);
        }

        private Weapon(WeaponSO weaponSO, Dictionary<Type, Characteristic> stats) : base(weaponSO.name, stats) {
            WeaponData = weaponSO;
        }

        public float GetDamage() => Stats[Type.Damage].Value;

        public override EquipmentSlot.Type RequiredSlot() {
            return EquipmentSlot.Type.Weapon;
        }

        private List<Ray> _rays = new List<Ray>();

        public ShotResult TryShoot(float time, Vector3 muzzleHolePos, Quaternion muzzleHoleRot, out List<Ray> rays) {
            _rays.Clear();
            rays = _rays;
            if (time - LastShotTime < 1 / WeaponData.shotsPerSecond) {
                return ShotResult.TooFast;
            }

            if (WeaponData.allAmmo == 0) {
                LastShotTime = time;
                return ShotResult.NoAmmoInBackpack;
            }

            if (WeaponData.currentBulletsInMagazine == 0 || WeaponData.isReloading) {
                LastShotTime = time;
                return ShotResult.NoAmmoInMagazine;
            }

            WeaponData.currentBulletsInMagazine -= 1;
            WeaponData.allAmmo -= 1;
            LastShotTime = time;
            OnShot?.Invoke();

            for (var i = 0; i < WeaponData.bulletsPerShot; i++) {
                var localShootDir = muzzleHoleRot * SimpsonsSpreading.Spreading(WeaponData.shotSpread);
                localShootDir.y = 0;
                rays.Add(new Ray(muzzleHolePos, localShootDir));
            }

            return ShotResult.ShotSuccessful;
        }

        public IEnumerator Reload() {
            OnReloadStart?.Invoke();
            WeaponData.isReloading = true;
            yield return new WaitForSeconds(WeaponData.reloadTime);
            WeaponData.currentBulletsInMagazine = WeaponData.allAmmo >= WeaponData.maxBulletsInMagazine
                ? WeaponData.maxBulletsInMagazine
                : WeaponData.allAmmo % WeaponData.maxBulletsInMagazine;
            OnReloadEnd?.Invoke();
            WeaponData.isReloading = false;
        }
    }
}