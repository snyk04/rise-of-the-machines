using Classes.TryInHierarchie;
using UnityEngine;

namespace Classes
{
    public class Player
    {
        public enum State
        {
            Human,
            Robot
        }

        public static Player Instance { get; private set; }

        public delegate void TakeDamage(float damage);
        public delegate void RestoreHealth(float heal);

        public TakeDamage takeDamage { get; private set; }
        public RestoreHealth restoreHealth { get; private set; }
        
        public HealthCharacteristic Health { get; private set; }
        public SpeedCharacteristic MoveSpeed { get; private set; }
        public ArmorCharacteristic Armor { get; private set; }
        public Transform Transform { get; private set; }
        public Animator Animator { get; private set; }
        public CharacterController CharacterController { get; private set; }
        public Transform GunTransform { get; private set; }

        private State currentState;
        private Human human;
        private Robot robot;

        public Human Human
        {
            get => human;
            private set { human = value; }
        }
        public Robot Robot
        {
            get => robot;
            private set { robot = value; }
        }

        public State CurrentState
        {
            get => currentState;
            set
            {
                if (currentState == value) return;
                currentState = value;
                OnChangeState();
            }
        }

        public Player(Human human, Robot robot)
        {
            Human = human;
            Robot = robot;
            TransformIntoHuman();
            Instance = this;
        }

        private void OnChangeState()
        {
            if (currentState == State.Human)
            {
                TransformIntoHuman();
            }
            else
            {
                TransformIntoRobot();
            }
        }

        private void TransformIntoHuman()
        {
            TransformIntoObject(Human.TakeDamage, Human.RestoreHealth, Human.Health, Human.MoveSpeed, Human.Armor, Human.Transform, Human.Animator, Human.CharacterController, human.GunTransform);
        }
        private void TransformIntoRobot()
        {
            TransformIntoObject(Robot.TakeDamage, Robot.RestoreHealth, Robot.Health, Robot.MoveSpeed, Robot.Armor, Robot.Transform, Robot.Animator, Robot.CharacterController, Robot.GunTransform);
        }
        private void TransformIntoObject(TakeDamage objTakeDamage, RestoreHealth objRestoreHealth, HealthCharacteristic objHealth, SpeedCharacteristic objMoveSpeed, ArmorCharacteristic objArmor, Transform objTransform, Animator objAnimator, CharacterController objCharacterController, Transform objGunTransform)
        {
            takeDamage = objTakeDamage;
            restoreHealth = objRestoreHealth;
            Health = objHealth;
            MoveSpeed = objMoveSpeed;
            Armor = objArmor;
            Transform = objTransform;
            Animator = objAnimator;
            CharacterController = objCharacterController;
            GunTransform = objGunTransform;
        }
    }
}
