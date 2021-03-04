namespace Classes {
    public class Player {
        public enum State {
            Human,
            Robot
        }

        public static Player player;

        private delegate void TakeDamage(float damage);

        private delegate void RestoreHealth(float heal);

        private TakeDamage takeDamage;
        private RestoreHealth restoreHealth;
        public float MoveSpeed { get; private set; }


        private State currentState;
        private Human human;
        private Robot robot;

        public Human Human {
            get => human;
            private set { human = value; }
        }

        public Robot Robot {
            get => robot;
            private set { robot = value; }
        }

        public State CurrentState {
            get => currentState;
            set {
                if (currentState == value) return;
                currentState = value;
                OnChangeState();
            }
        }

        public Player(Human human, Robot robot) {
            Human = human;
            Robot = robot;
            TransformIntoHuman();
            player = this;
        }

        private void OnChangeState() {
            if (currentState == State.Human) {
                TransformIntoHuman();
            }
            else {
                TransformIntoRobot();
            }
        }

        private void TransformIntoHuman() {
            TransformIntoObject(Human.TakeDamage, Human.RestoreHealth, Human.MoveSpeed);
        }

        private void TransformIntoRobot() {
            TransformIntoObject(Robot.TakeDamage, Robot.RestoreHealth, Robot.MoveSpeed);
        }

        private void TransformIntoObject(TakeDamage objTakeDamage, RestoreHealth objRestoreHealth, float objMoveSpeed) {
            takeDamage = objTakeDamage;
            restoreHealth = objRestoreHealth;
            MoveSpeed = objMoveSpeed;
        }
    }
}