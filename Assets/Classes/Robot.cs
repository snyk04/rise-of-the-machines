using System.Linq;
using Classes.TryInHierarchie;
using UnityEngine;
using Type = Classes.TryInHierarchie.Characteristic.Type;

namespace Classes {
    public class Robot : Person {
        public delegate void CharacteristicsChanged();

        public event CharacteristicsChanged CharacteristicsChangedEvent;

        public CharacterController CharacterController { get; }
        public Transform GunTransform { get; }

        private readonly EquipmentSlot[] slots = {new HeadSlot(), new ChestSlot(), new LegsSlot(), new WeaponSlot()};

        public Robot(float maxHealth, float moveSpeed, float armor, Transform transform, Animator animator,
            CharacterController characterController, Transform gunTransform) : base(maxHealth, armor, moveSpeed,
            transform,
            animator) {
            CharacterController = characterController;
            GunTransform = gunTransform;
            Stats.Add(Type.Health, Health);
            Stats.Add(Type.Speed, MoveSpeed);
            Stats.Add(Type.Armor, Armor);
        }

        public void ChangeSlotsEquipment(Equipment newItem) {
            Equipment oldItem;
            switch (newItem) {
                case HeadArmor _:
                    if (slots.First(slot => slot is HeadSlot).TryChangeItem(newItem, out oldItem)) {
                        RecalculateCharacteristics(oldItem, newItem);
                    }

                    break;
                case ChestArmor _:
                    if (slots.First(slot => slot is ChestSlot).TryChangeItem(newItem, out oldItem)) {
                        RecalculateCharacteristics(oldItem, newItem);
                    }

                    break;
                case LegsArmor _:
                    if (slots.First(slot => slot is LegsSlot).TryChangeItem(newItem, out oldItem)) {
                        RecalculateCharacteristics(oldItem, newItem);
                    }

                    break;
                case Weapon _:
                    if (slots.First(slot => slot is WeaponSlot).TryChangeItem(newItem, out oldItem)) {
                        RecalculateCharacteristics(oldItem, newItem);
                    }

                    break;
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