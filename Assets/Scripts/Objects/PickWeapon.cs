using System;
using System.Collections.Generic;
using Classes;
using Classes.ScriptableObjects;
using Classes.TryInHierarchie;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Objects {
    public class PickWeapon : PickObject {
        [SerializeField] private WeaponSO WeaponSO;

        void Start() {
            item = Weapon.CreateWeapon(WeaponSO);
        }

        private void Update() {
            // Debug.Log($"{(Player.Instance.Inventory.GetContent().Count > 0 ? Player.Instance.Inventory.GetContent()[0] : null)}");
            if (Mouse.current.leftButton.wasPressedThisFrame) {
                TryPickObject();
            }
        }
    }
}