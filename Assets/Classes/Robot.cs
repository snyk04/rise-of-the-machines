using System.Collections.Generic;
using System.Linq;
using Classes.TryInHierarchie;
using UnityEngine;
using Type = Classes.TryInHierarchie.EquipmentSlot.Type;

namespace Classes {
    public class Robot : Person {
        public delegate void CharacteristicsChanged();

        public event CharacteristicsChanged CharacteristicsChangedEvent;
        
        public Player.UnityData UnityRobotData { get; private set; }

        public readonly Dictionary<Type, EquipmentSlot> equipmentSlots = new Dictionary<Type, EquipmentSlot> {
            {Type.Head, new HeadSlot()},
            {Type.Chest, new ChestSlot()},
            {Type.Legs, new LegsSlot()}
        };

        public readonly WeaponsSlots weaponsSlots = new WeaponsSlots(new Dictionary<WeaponSlot.Spot, WeaponSlot> {
            {WeaponSlot.Spot.TwoHands, new WeaponSlot(WeaponSlot.Spot.TwoHands)}
        });

        public Robot(float maxHealth, float moveSpeed, float armor, Transform transform, Animator animator,
            CharacterController characterController, Transform gunTransform) : base(maxHealth, armor, moveSpeed) {
            UnityRobotData = new Player.UnityData();
            UnityRobotData.Initialize(transform, animator, characterController, gunTransform);
        }

        public Robot(float maxHealth, float moveSpeed, float armor) : base(maxHealth, armor, moveSpeed) {
            UnityRobotData = new Player.UnityData();
        }
        
        public Transform Transform => UnityRobotData.Transform;
        public Animator Animator => UnityRobotData.Animator;
        public CharacterController CharacterController => UnityRobotData.CharacterController;
        public Transform GunTransform => UnityRobotData.GunTransform;

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
            if (weaponsSlots.TryChangeItem(newWeapon, spot, out var oldWeapon)) {
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