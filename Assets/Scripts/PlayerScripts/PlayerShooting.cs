using InputHandling;
using Objects;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GunController gun;

        private InputCombat input;
        private bool IsShooting { get; set; }

        private void Start()
        {
            input = InputCombat.Instance;

            input.combatActions.Reload.performed += context => Reload();
            input.combatActions.StartShooting.performed += context => StartShooting();
            input.combatActions.StopShooting.performed += context => StopShooting();
        }
        private void Update()
        {
            if (IsShooting)
            {
                gun.TryShoot();
                if (!gun.weapon.WeaponData.isAutomatic)
                {
                    IsShooting = false;
                }
            }
        }

        private void Reload()
        {
            gun.Reload();
        }
        private void StartShooting()
        {
            IsShooting = true;
        }
        private void StopShooting()
        {
            IsShooting = false;
        }
    }
}
