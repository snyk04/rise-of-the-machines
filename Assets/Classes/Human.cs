using UnityEngine;

namespace Classes {
    public class Human : Person {
        public CharacterController CharacterController { get; }
        public Transform GunTransform { get; }

        public Human(float maxHealth, float moveSpeed, Transform transform, Animator animator,
            CharacterController characterController, Transform gunTransform) : base(maxHealth, moveSpeed, transform, animator) {
            CharacterController = characterController;
            GunTransform = gunTransform;
        }
    }
}