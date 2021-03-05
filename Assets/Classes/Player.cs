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

        public static Player player;

        private delegate void TakeDamage(float damage);
        private delegate void RestoreHealth(float heal);

        private TakeDamage takeDamage;
        private RestoreHealth restoreHealth;
        public float MoveSpeed { get; private set; }
        public Transform Transform { get; private set; }

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
            player = this;
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
            TransformIntoObject(Human.TakeDamage, Human.RestoreHealth, Human.MoveSpeed, Human.Transform);
        }
        private void TransformIntoRobot()
        {
            TransformIntoObject(Robot.TakeDamage, Robot.RestoreHealth, Robot.MoveSpeed, Robot.Transform);
        }
        private void TransformIntoObject(TakeDamage objTakeDamage, RestoreHealth objRestoreHealth, float objMoveSpeed, Transform objTransform)
        {
            takeDamage = objTakeDamage;
            restoreHealth = objRestoreHealth;
            MoveSpeed = objMoveSpeed;
            Transform = objTransform;
        }
    }
}
