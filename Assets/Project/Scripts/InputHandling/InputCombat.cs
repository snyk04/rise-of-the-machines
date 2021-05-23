using UnityEngine;

namespace Project.Scripts.InputHandling
{
    public class InputCombat : MonoBehaviour
    {
        public Controls.CombatActions combatActions;
        public static InputCombat Instance;

        private void Awake()
        {
            combatActions = Input.Controls.Combat;
            Instance = this;
        }
        private void OnEnable()
        {
            EnableControls();
        }
        private void OnDisable()
        {
            DisableControls();
        }

        public void EnableControls()
        {
            combatActions.Enable();
        }
        public void DisableControls()
        {
            combatActions.Disable();
        }
    }
}
