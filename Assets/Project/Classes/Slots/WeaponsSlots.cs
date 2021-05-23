using System.Collections.Generic;
using Project.Classes.ItemsAndInventory.Equipment;
using UnityEngine;

namespace Project.Classes.Slots {
    public class WeaponsSlots {
        public Dictionary<WeaponSlot.Spot, WeaponSlot> Slots { get; private set; }

        public WeaponsSlots(Dictionary<WeaponSlot.Spot, WeaponSlot> slots) {
            Slots = slots;
        }


        public bool TryChangeItem(Weapon newWeapon, WeaponSlot.Spot spot, out Equipment oldWeapon) {
            var result = false;
            oldWeapon = null;
            switch (spot) {
                case WeaponSlot.Spot.TwoHands:
                    if (Slots.ContainsKey(WeaponSlot.Spot.TwoHands)) {
                        result = Slots[WeaponSlot.Spot.TwoHands].TryChangeItem(newWeapon, out oldWeapon);
                        if (!result) break;
                        Debug.Log($"{oldWeapon} dropped");
                        if (Slots.ContainsKey(WeaponSlot.Spot.LeftHand)) {
                            Debug.Log($"{Slots[WeaponSlot.Spot.LeftHand]} dropped");
                            Slots[WeaponSlot.Spot.LeftHand] = null;
                        }

                        if (Slots.ContainsKey(WeaponSlot.Spot.RightHand)) {
                            Debug.Log($"{Slots[WeaponSlot.Spot.RightHand]} dropped");
                            Slots[WeaponSlot.Spot.RightHand] = null;
                        }
                    }
                    else {
                        Debug.Log($"There is no {spot} spot");
                    }

                    break;
                default:
                    if (Slots.ContainsKey(spot)) {
                        result = Slots[WeaponSlot.Spot.TwoHands].TryChangeItem(newWeapon, out oldWeapon);
                        if (!result) break;
                        Debug.Log($"{oldWeapon} dropped");
                    }
                    else {
                        Debug.Log($"There is no {spot} spot");
                    }

                    break;
            }

            return result;
        }
    }
}