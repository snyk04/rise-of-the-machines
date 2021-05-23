using Project.Classes.ItemsAndInventory.Equipment;
using Project.Classes.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Objects {
    public class PickWeapon : PickObject {
        [SerializeField] private WeaponSO WeaponSO;

        void Start() {
            item = Weapon.CreateWeapon(WeaponSO);
        }

        private void Update() {
            if (Mouse.current.leftButton.wasPressedThisFrame) {
                TryPickObject();
            }
        }
    }
}