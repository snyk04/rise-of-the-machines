using UnityEngine;

namespace Project.Classes.Damagеable {
    public class Human : Person {
        public Player.UnityData UnityHumanData { get; private set; }

        public Human(float maxHealth, float moveSpeed, float armor, Transform transform, Animator animator,
            CharacterController characterController, Transform gunTransform) : base(maxHealth, moveSpeed, armor) {
            UnityHumanData = new Player.UnityData();
            UnityHumanData.Initialize(transform, animator, characterController, gunTransform);
        }
        public Human(float maxHealth, float moveSpeed, float armor) : base(maxHealth, moveSpeed, armor) {
            UnityHumanData = new Player.UnityData();
        }

        public Transform Transform => UnityHumanData.Transform;
        public Animator Animator => UnityHumanData.Animator;
        public CharacterController CharacterController => UnityHumanData.CharacterController;
        public Transform GunTransform => UnityHumanData.GunTransform;
    }
}