using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UserInterface {
    public class ButtonContainer : MonoBehaviour
    {
        [SerializeField] private Button[] buttons;

        private void Start()
        {
            buttons[0]?.Select();
        }
    }
}
