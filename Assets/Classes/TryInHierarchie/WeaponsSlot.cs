using System;
using System.Collections.Generic;
using UnityEngine;

namespace Classes.TryInHierarchie {
    public class WeaponsSlot {
        public Dictionary<WeaponSlot.Spot, WeaponSlot> Slots { get; private set; }

        public WeaponsSlot(Dictionary<WeaponSlot.Spot, WeaponSlot> slots) {
            Slots = slots;
        }

        public bool TryChangeItem(Weapon weapon) {
            var result = false;
            switch (weapon.WeaponData.spot) {
                case WeaponSlot.Spot.TwoHands:
                    if (Slots.ContainsKey(WeaponSlot.Spot.TwoHands)) {
                        result = Slots[WeaponSlot.Spot.TwoHands].TryChangeItem(weapon, out var oldItem);
                        if (!result) break;
                        Debug.Log($"{oldItem} dropped");
                        if (Slots.ContainsKey(WeaponSlot.Spot.LeftHand)) {
                            Debug.Log($"{Slots[WeaponSlot.Spot.LeftHand]} dropped");
                            Slots[WeaponSlot.Spot.LeftHand] = null;
                        }

                        if (Slots.ContainsKey(WeaponSlot.Spot.RightHand)) {
                            Debug.Log($"{Slots[WeaponSlot.Spot.RightHand]} dropped");
                            Slots[WeaponSlot.Spot.RightHand] = null;
                        }
                    }

                    break;
                default:
                    if (Slots.ContainsKey(weapon.WeaponData.spot)) {
                        result = Slots[WeaponSlot.Spot.TwoHands].TryChangeItem(weapon, out var oldItem);
                        if (!result) break;
                        Debug.Log($"{oldItem} dropped");
                    }

                    break;
            }

            return result;
        }
    }
}