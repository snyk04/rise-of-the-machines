using System;
using Objects;
using PlayerScripts;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class AmmoCounter : MonoBehaviour
    {
        public static AmmoCounter Instance;
        
        [SerializeField] private TextMeshProUGUI leftGunText;
        [SerializeField] private TextMeshProUGUI rightGunText;
        [SerializeField] private PlayerShooting playerShooting;

        private GunController leftGun;
        private GunController rightGun;

        private void Awake()
        {
            Instance = this;
        }

        public void SubscribeToGuns()
        {
            leftGun = playerShooting.LeftHandGun;
            rightGun = playerShooting.RightHandGun;
            
            if (leftGun)
            {
                var leftWeapon = leftGun.Weapon;
                leftWeapon.OnShot -= UpdateAmmoLeft;
                leftWeapon.OnReloadEnd -= UpdateAmmoLeft;
                leftWeapon.OnShot += UpdateAmmoLeft;
                leftWeapon.OnReloadEnd += UpdateAmmoLeft;
                
                UpdateAmmoLeft();
            }
            else
            {
                leftGunText.text = "";
            }
            
            if (rightGun)
            {
                var rightWeapon = rightGun.Weapon;
                rightWeapon.OnShot -= UpdateAmmoRight;
                rightWeapon.OnReloadEnd -= UpdateAmmoRight;
                rightWeapon.OnShot += UpdateAmmoRight;
                rightWeapon.OnReloadEnd += UpdateAmmoRight;
                
                UpdateAmmoRight();
            }
            else
            {
                rightGunText.text = "";
            }
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
