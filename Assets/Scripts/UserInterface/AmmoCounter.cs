using Objects;
using PlayerScripts;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class AmmoCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI leftGunText;
        [SerializeField] private TextMeshProUGUI rightGunText;
        [SerializeField] private PlayerShooting playerShooting;

        private GunController leftGun;
        private GunController rightGun;

        private void Awake()
        {
            leftGun = playerShooting.LeftGun;
            rightGun = playerShooting.RightGun;
        }
        private void Start()
        {
            var leftWeapon = leftGun.Weapon;
            var rightWeapon = rightGun.Weapon;
            var leftWeaponData = leftWeapon.WeaponData;
            var rightWeaponData = rightWeapon.WeaponData;

            leftWeapon.OnShot += UpdateAmmoLeft;
            rightWeapon.OnShot += UpdateAmmoRight;
            leftWeapon.OnReloadEnd += UpdateAmmoLeft;
            rightWeapon.OnReloadEnd += UpdateAmmoRight;

            UpdateAmmoLeft();
            UpdateAmmoRight();
        }

        private void UpdateAmmoLeft()
        {
            leftGunText.text = $"{leftGun.Weapon.WeaponData.currentBulletsInMagazine} / {leftGun.Weapon.WeaponData.allAmmo - leftGun.Weapon.WeaponData.currentBulletsInMagazine}";
        }
        private void UpdateAmmoRight()
        {
            rightGunText.text = $"{rightGun.Weapon.WeaponData.currentBulletsInMagazine} / {rightGun.Weapon.WeaponData.allAmmo - rightGun.Weapon.WeaponData.currentBulletsInMagazine}";
        }
    }
}
