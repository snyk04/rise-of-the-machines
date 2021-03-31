using System.Linq;
using Classes.TryInHierarchie;
using UnityEngine;

namespace Classes {
    public class Robot : Person {
        public delegate void CharacteristicsChanged();

        public event CharacteristicsChanged CharacteristicsChangedEvent;

        public CharacterController CharacterController { get; }
        public Transform GunTransform { get; }

        private readonly EquipmentSlot[] slots = {new HeadSlot(), new ChestSlot(), new LegsSlot()};

        public Robot(float maxHealth, float moveSpeed, Transform transform, Animator animator,
            CharacterController characterController, Transform gunTransform) : base(maxHealth, moveSpeed, transform,
            animator) {
            CharacterController = characterController;
            GunTransform = gunTransform;
        }

        public void ChangeSlotsEquipment(Equipment newItem) {
            switch (newItem) {
                case HeadArmor _:
                    slots.First(slot => slot is HeadSlot).TryChangeItem(newItem, out _);
                    break;
                case ChestArmor _:
                    slots.First(slot => slot is ChestSlot).TryChangeItem(newItem, out _);
                    break;
                case LegsArmor _:
                    slots.First(slot => slot is LegsSlot).TryChangeItem(newItem, out _);
                    break;
            }
        }

        private void RecalculateCharacteristics() { // todo recalculate characteristics
            CharacteristicsChangedEvent?.Invoke();
            
        }
    }
}