using UnityEngine;

namespace InputHandling
{
    public class InputCombat : MonoBehaviour
    {
        public Controls.CombatActions combatActions;
        public static InputCombat Instance;

        private void Awake()
        {
            combatActions = Input.Controls.Combat;
            Instance = this;

            SetControls();
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
        private void SetControls()
        {
        }
    }
}
