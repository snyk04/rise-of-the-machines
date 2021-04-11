using System;
using System.Collections.Generic;
using UnityEngine;

namespace Classes.TryInHierarchie {
    public class WeaponsSlot {
        public Dictionary<WeaponSlot.Type, WeaponSlot> Slots { get; private set; }

        public WeaponsSlot(Dictionary<WeaponSlot.Type, WeaponSlot> slots) {
            Slots = slots;
        }

        public bool TryChangeItem(Weapon weapon) {
            var result = false;
            switch (weapon.WeaponData.type) {
                case WeaponSlot.Type.TwoHands:
                    if (Slots.ContainsKey(WeaponSlot.Type.TwoHands)) {
                        result = Slots[WeaponSlot.Type.TwoHands].TryChangeItem(weapon, out var oldItem);
                        if (!result) break;
                        Debug.Log($"{oldItem} dropped");
                        if (Slots.ContainsKey(WeaponSlot.Type.LeftHand)) {
                            Debug.Log($"{Slots[WeaponSlot.Type.LeftHand]} dropped");
                            Slots[WeaponSlot.Type.LeftHand] = null;
                        }

                        if (Slots.ContainsKey(WeaponSlot.Type.RightHand)) {
                            Debug.Log($"{Slots[WeaponSlot.Type.RightHand]} dropped");
                            Slots[WeaponSlot.Type.RightHand] = null;
                        }
                    }

                    break;
                default:
                    if (Slots.ContainsKey(weapon.WeaponData.type)) {
                        result = Slots[WeaponSlot.Type.TwoHands].TryChangeItem(weapon, out var oldItem);
                        if (!result) break;
                        Debug.Log($"{oldItem} dropped");
                    }

                    break;
            }

            return result;
        }
    }
}