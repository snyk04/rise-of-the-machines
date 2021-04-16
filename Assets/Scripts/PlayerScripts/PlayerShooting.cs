using Objects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private GunController gun;

        private void Update()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.leftButton.isPressed && gun.weapon.WeaponData.isAutomatic)
            {
                gun.Shoot();
            }
        }
    }
}
