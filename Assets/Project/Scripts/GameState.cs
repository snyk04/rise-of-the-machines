using UnityEngine;

namespace Project.Scripts {
    public class GameState : MonoBehaviour
    {
        public static GameState Instance;
        private int amountOfTriggeredEnemies = 0;
        public bool IsBattle => amountOfTriggeredEnemies > 0;

        public delegate void ChangeStateEvent();
        public event ChangeStateEvent OnCombatStart;
        public event ChangeStateEvent OnCombatEnd;

        public void AddTriggeredEnemies(int amountOfEnemies)
        {
            if (amountOfTriggeredEnemies == 0)
            {
                OnCombatStart.Invoke();
            }
            amountOfTriggeredEnemies += amountOfEnemies;
        }
        public void RemoveTriggeredEnemies(int amountOfEnemies)
        {
            if (amountOfTriggeredEnemies == 1)
            {
                OnCombatEnd.Invoke();
            }
            amountOfTriggeredEnemies -= amountOfEnemies;
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}
