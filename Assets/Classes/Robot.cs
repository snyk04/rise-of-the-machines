using UnityEngine;

namespace Classes {
    public class Robot : Person {
        public CharacterController CharacterController { get; }
        public Transform GunTransform { get; }

        public Robot(float maxHealth, float moveSpeed, Transform transform, Animator animator,
            CharacterController characterController, Transform gunTransform) : base(maxHealth, moveSpeed, transform, animator) {
            CharacterController = characterController;
            GunTransform = gunTransform;
        }
    }
}