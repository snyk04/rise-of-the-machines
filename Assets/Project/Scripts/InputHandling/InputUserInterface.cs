using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.InputHandling
{
    public class InputUserInterface : MonoBehaviour
    {
        public Controls.UserInterfaceActions userInterfaceActions;
        public static InputUserInterface Instance;

        private void Awake()
        {
            userInterfaceActions = Input.Controls.UserInterface;
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
            userInterfaceActions.Enable();
        }
        public void DisableControls()
        {
            userInterfaceActions.Disable();
        }
    }
}
