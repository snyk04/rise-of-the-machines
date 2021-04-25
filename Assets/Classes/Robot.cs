using System.Collections.Generic;
using System.Linq;
using Classes.TryInHierarchie;
using UnityEngine;
using Type = Classes.TryInHierarchie.EquipmentSlot.Type;

namespace Classes {
    public class Robot : Person {
        public delegate void CharacteristicsChanged();

        public event CharacteristicsChanged CharacteristicsChangedEvent;

        public CharacterController CharacterController { get; }
        public Transform GunTransform { get; }

        private readonly Dictionary<Type, EquipmentSlot> equipmentSlots = new Dictionary<Type, EquipmentSlot> {
            {Type.Head, new HeadSlot()},
            {Type.Chest, new ChestSlot()},
            {Type.Legs, new LegsSlot()}
        };

        private readonly WeaponsSlot weaponsSlot = new WeaponsSlot(new Dictionary<WeaponSlot.Spot, WeaponSlot> {
            {WeaponSlot.Spot.TwoHands, new WeaponSlot(WeaponSlot.Spot.TwoHands)}
        });

        public Robot(float maxHealth, float moveSpeed, float armor, Transform transform, Animator animator,
            CharacterController characterController, Transform gunTransform) : base(maxHealth, armor, moveSpeed,
            transform,
            animator) {
            CharacterController = characterController;
            GunTransform = gunTransform;
        }

        public void ChangeArmor(Equipment newItem) {
            Equipment oldItem;
            switch (newItem) {
                case HeadArmor _:

                    if (equipmentSlots[Type.Head].TryChangeItem(newItem, out oldItem)) {
                        RecalculateCharacteristics(oldItem, newItem);
                    }

                    break;
                case ChestArmor _:
                    if (equipmentSlots[Type.Chest].TryChangeItem(newItem, out oldItem)) {
                        RecalculateCharacteristics(oldItem, newItem);
                    }

                    break;
                case LegsArmor _:
                    if (equipmentSlots[Type.Legs].TryChangeItem(newItem, out oldItem)) {
                        RecalculateCharacteristics(oldItem, newItem);
                    }

                    break;
                default:
                    Debug.Log("There is no such slot");
                    break;
            }
        }

        public void ChangeWeapon(Weapon newWeapon, WeaponSlot.Spot spot) {
            if (weaponsSlot.TryChangeItem(newWeapon, spot, out var oldWeapon)) {
                RecalculateCharacteristics(oldWeapon, newWeapon);
            }
        }

        private void RecalculateCharacteristics(Equipment oldItem, Equipment newItem) {
            var oldItemStatsKeys = oldItem.Stats.Keys.ToList();
            var newItemStatsKeys = newItem.Stats.Keys.ToList();
            foreach (var key in oldItemStatsKeys) {
                Stats[key].Value -= oldItem.Stats[key].Value;
            }

            foreach (var key in newItemStatsKeys) {
                if (Stats.ContainsKey(key)) {
                    Stats[key].Value += newItem.Stats[key].Value;
                }
                else {
                    Stats.Add(key, newItem.Stats[key]);
                }
            }

            CharacteristicsChangedEvent?.Invoke();
        }
    }
}