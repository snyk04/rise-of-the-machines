using Classes.ScriptableObjects;
using Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class AmmoCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private GunController gun;

        private WeaponSO weaponData;

        private void Start()
        {
            var weapon = gun.weapon;
            weapon.OnShot += UpdateAmmoInMagazine;
            weapon.OnReloadEnd += UpdateAmmoInBackpack;

            weaponData = weapon.WeaponData;
            text.text = $"{weaponData.currentBulletsInMagazine} / {weaponData.allAmmo - weaponData.currentBulletsInMagazine}";
        }

        private void UpdateAmmoInMagazine()
        {
            text.text = $"{weaponData.currentBulletsInMagazine} / {weaponData.allAmmo - weaponData.currentBulletsInMagazine}";
        }
        private void UpdateAmmoInBackpack()
        {
            text.text = $"{weaponData.currentBulletsInMagazine} / {weaponData.allAmmo - weaponData.currentBulletsInMagazine}";
        }
    }
}
