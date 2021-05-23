using Project.Classes.Damagеable;
using UnityEngine;

namespace Project.Scripts {
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            var player = new Player(new Human(100, 4, 20),
                new Robot(100, 4, 20)); // todo add HumanSO, RobotSO
        }
    }
}
