using System;
using Project.Classes.Damagable;
using Project.Scripts.InputHandling;
using Project.Scripts.Objects;
using Project.Scripts.UserInterface;
using UnityEngine;

namespace Project.Scripts.PlayerScripts
{
    public class PlayerShooting : MonoBehaviour
    {
        #region Properties

        public static PlayerShooting Instance;

        [SerializeField] private GunController humanLeftHandGun;
        [SerializeField] private GunController humanRightHandGun;
        [SerializeField] private SmartGunController humanLeftShoulderGun;
        [SerializeField] private SmartGunController humanRightShoulderGun;
        
        [Space]
        
        [SerializeField] private GunController robotLeftHandGun;
        [SerializeField] private GunController robotRightHandGun;
        [SerializeField] private SmartGunController robotLeftShoulderGun;
        [SerializeField] private SmartGunController robotRightShoulderGun;
        
        public GunController LeftHandGun { get; private set; }
        public GunController RightHandGun { get; private set; }
        public SmartGunController LeftShoulderGun { get; private set; }
        public SmartGunController RightShoulderGun { get; private set; }

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
            
            ChangeShooter();

            input.combatActions.Reload.performed += context => Reload();
            input.combatActions.StartShootingLeft.performed += context => StartShootingLeft();
            input.combatActions.StopShootingLeft.performed += context => StopShootingLeft();
            input.combatActions.StartShootingRight.performed += context => StartShootingRight();
            input.combatActions.StopShootingRight.performed += context => StopShootingRight();
            input.combatActions.ShootLeftShoulder.performed += context => LeftShoulderGun.ShootABurst();
            input.combatActions.ShootRightShoulder.performed += context => RightShoulderGun.ShootABurst();

            Player.Instance.stateChangedEvent += ChangeShooter;
        }
        private void Update()
        {
            if (IsLeftGunShooting)
            {
                LeftHandGun.TryShoot();
                if (!LeftHandGun.Weapon.WeaponData.isAutomatic)
                {
                    StopShootingLeft();
                }
            }
            if (IsRightGunShooting)
            {
                RightHandGun.TryShoot();
                if (!RightHandGun.Weapon.WeaponData.isAutomatic)
                {
                    StopShootingRight();
                }
            }

        }

        #endregion

        #region Methods

        private void Reload()
        {
            LeftHandGun.Reload();
            RightHandGun.Reload();
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

        private void ChangeShooter()
        {
            switch (Player.Instance.CurrentState)
            {
                case Player.State.Human:
                    LeftHandGun = humanLeftHandGun;
                    RightHandGun = humanRightHandGun;
                    LeftShoulderGun = humanLeftShoulderGun;
                    RightShoulderGun = humanRightShoulderGun;
                    break;
                case Player.State.Robot:
                    LeftHandGun = robotLeftHandGun;
                    RightHandGun = robotRightHandGun;
                    LeftShoulderGun = robotLeftShoulderGun;
                    RightShoulderGun = robotRightShoulderGun;                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            AmmoCounter.Instance.SubscribeToGuns();

            if (LeftHandGun)
            {
                LeftHandGun.Weapon.OnShot -= InvokeShoot;
                LeftHandGun.Weapon.OnShot += InvokeShoot;
            }
            if (RightHandGun)
            {
                RightHandGun.Weapon.OnShot -= InvokeShoot;
                RightHandGun.Weapon.OnShot += InvokeShoot;
            }
        }

        private void InvokeShoot()
        {
            OnShot?.Invoke();
        }

        #endregion
    }
}
