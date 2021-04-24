using Classes.ScriptableObjects;
using Objects;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class AmmoCounter : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private GunController gun;

        private WeaponSO weaponData;

        private void Start()
        {
            var weapon = gun.weapon;
            weapon.OnShot += UpdateAmmoInMagazine;
            weapon.OnReload += UpdateAmmoInBackpack;

            weaponData = weapon.WeaponData;
            text.text = $"{weaponData.currentBulletsInMagazine} / {weaponData.allAmmo - weaponData.currentBulletsInMagazine}";
        }

        public void UpdateAmmoInMagazine()
        {
            text.text = $"{weaponData.currentBulletsInMagazine} / {weaponData.allAmmo - weaponData.currentBulletsInMagazine}";
        }
        public void UpdateAmmoInBackpack()
        {
            text.text = $"{weaponData.currentBulletsInMagazine} / {weaponData.allAmmo - weaponData.currentBulletsInMagazine}";
        }
    }
}
