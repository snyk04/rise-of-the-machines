using InputHandling;
using Objects;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GunController leftGun;
        [SerializeField] private GunController rightGun;

        public GunController LeftGun { get => leftGun; }
        public GunController RightGun { get => rightGun; }

        private InputCombat input;
        private bool IsLeftGunShooting { get; set; }
        private bool IsRightGunShooting { get; set; }

        private void Start()
        {
            input = InputCombat.Instance;

            input.combatActions.Reload.performed += context => Reload();
            input.combatActions.StartShootingLeft.performed += context => StartShootingLeft();
            input.combatActions.StopShootingLeft.performed += context => StopShootingLeft();
            input.combatActions.StartShootingRight.performed += context => StartShootingRight();
            input.combatActions.StopShootingRight.performed += context => StopShootingRight();
        }
        private void Update()
        {
            if (IsLeftGunShooting)
            {
                leftGun.TryShoot();
                if (!leftGun.weapon.WeaponData.isAutomatic)
                {
                    StopShootingLeft();
                }
            }
            if (IsRightGunShooting)
            {
                rightGun.TryShoot();
                if (!rightGun.weapon.WeaponData.isAutomatic)
                {
                    StopShootingRight();
                }
            }

        }

        private void Reload()
        {
            leftGun.Reload();
            rightGun.Reload();
        }

        private void StartShootingLeft()
        {
            IsLeftGunShooting = true;
        }
        private void StopShootingLeft()
        {
            IsLeftGunShooting = false;
        }
        private void StartShootingRight()
        {
            IsRightGunShooting = true;
        }
        private void StopShootingRight()
        {
            IsRightGunShooting = false;
        }
    }
}
