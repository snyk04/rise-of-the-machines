using UnityEngine;
using Type = Classes.TryInHierarchie.Characteristic.Type;


namespace Classes {
    public class Human : Person {
        public CharacterController CharacterController { get; }
        public Transform GunTransform { get; }

        public Human(float maxHealth, float moveSpeed, float armor, Transform transform, Animator animator,
            CharacterController characterController, Transform gunTransform) : base(maxHealth, moveSpeed, armor, transform, animator) {
            CharacterController = characterController;
            GunTransform = gunTransform;
        }
    }
}