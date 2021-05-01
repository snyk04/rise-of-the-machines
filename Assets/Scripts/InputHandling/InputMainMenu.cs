using UnityEngine;

namespace InputHandling
{
    public class InputMainMenu : MonoBehaviour
    {
        public Controls.MainMenuActions mainMenuActions;
        public static InputMainMenu Instance;

        private void Awake()
        {
            mainMenuActions = Input.Controls.MainMenu;
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
            mainMenuActions.Enable();
        }
        public void DisableControls()
        {
            mainMenuActions.Disable();
        }
        private void SetControls()
        {
        }
    }
}
