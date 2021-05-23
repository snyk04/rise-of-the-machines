using UnityEngine;

namespace Project.Scripts.InputHandling
{
    public class InputInteraction : MonoBehaviour
    {
        public Controls.InteractionActions interactionActions;
        public static InputInteraction Instance;

        private void Awake()
        {
            interactionActions = Input.Controls.Interaction;
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
            interactionActions.Enable();
        }
        public void DisableControls()
        {
            interactionActions.Disable();
        }
    }
}
