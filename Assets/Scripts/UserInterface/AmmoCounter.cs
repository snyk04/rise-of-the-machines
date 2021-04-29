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
            var leftWeapon = leftGun.weapon;
            var rightWeapon = rightGun.weapon;
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
            leftGunText.text = $"{leftGun.weapon.WeaponData.currentBulletsInMagazine} / {leftGun.weapon.WeaponData.allAmmo - leftGun.weapon.WeaponData.currentBulletsInMagazine}";
        }
        private void UpdateAmmoRight()
        {
            rightGunText.text = $"{rightGun.weapon.WeaponData.currentBulletsInMagazine} / {rightGun.weapon.WeaponData.allAmmo - rightGun.weapon.WeaponData.currentBulletsInMagazine}";
        }
    }
}
