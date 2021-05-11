using InputHandling;
using Objects;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerShooting : MonoBehaviour
    {
        #region Properties

        public static PlayerShooting Instance;
        
        [SerializeField] private GunController leftHandWeapon;
        [SerializeField] private GunController rightHandWeapon;
        [SerializeField] private SmartGunController leftShoulderWeapon;
        [SerializeField] private SmartGunController rightShoulderWeapon;

        public GunController LeftHandWeapon => leftHandWeapon;
        public GunController RightHandWeapon => rightHandWeapon;
        
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
            input.combatActions.ShootLeftShoulder.performed += context => leftShoulderWeapon.ShootABurst();
            input.combatActions.ShootRightShoulder.performed += context => rightShoulderWeapon.ShootABurst();
            
            leftHandWeapon.Weapon.OnShot += () => { OnShot?.Invoke(); };
            rightHandWeapon.Weapon.OnShot += () => { OnShot?.Invoke(); };
        }
        private void Update()
        {
            if (IsLeftGunShooting)
            {
                leftHandWeapon.TryShoot();
                if (!leftHandWeapon.Weapon.WeaponData.isAutomatic)
                {
                    StopShootingLeft();
                }
            }
            if (IsRightGunShooting)
            {
                rightHandWeapon.TryShoot();
                if (!rightHandWeapon.Weapon.WeaponData.isAutomatic)
                {
                    StopShootingRight();
                }
            }

        }

        #endregion

        #region Methods

        private void Reload()
        {
            leftHandWeapon.Reload();
            rightHandWeapon.Reload();
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
