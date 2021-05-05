using System;
using InputHandling;
using Objects;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerShooting : MonoBehaviour
    {
        #region Properties

        public static PlayerShooting Instance;
        
        [SerializeField] private GunController leftGun;
        [SerializeField] private GunController rightGun;

        public GunController LeftGun => leftGun;
        public GunController RightGun => rightGun;
        
        private bool IsLeftGunShooting { get; set; }
        private bool IsRightGunShooting { get; set; }

        private InputCombat input;

        #endregion

        #region Events

        public delegate void GunAction();

        public GunAction OnShot;

        #endregion

        #region Behaviour methods

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            input = InputCombat.Instance;

            input.combatActions.Reload.performed += context => Reload();
            input.combatActions.StartShootingLeft.performed += context => StartShootingLeft();
            input.combatActions.StopShootingLeft.performed += context => StopShootingLeft();
            input.combatActions.StartShootingRight.performed += context => StartShootingRight();
            input.combatActions.StopShootingRight.performed += context => StopShootingRight();
            
            leftGun.Weapon.OnShot += () => { OnShot?.Invoke(); };
            rightGun.Weapon.OnShot += () => { OnShot?.Invoke(); };
        }
        private void Update()
        {
            if (IsLeftGunShooting)
            {
                leftGun.TryShoot();
                if (!leftGun.Weapon.WeaponData.isAutomatic)
                {
                    StopShootingLeft();
                }
            }
            if (IsRightGunShooting)
            {
                rightGun.TryShoot();
                if (!rightGun.Weapon.WeaponData.isAutomatic)
                {
                    StopShootingRight();
                }
            }

        }

        #endregion

        #region Methods

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

        #endregion
    }
}
