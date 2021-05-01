using UnityEngine;
using UnityEngine.UI;

public class ButtonContainer : MonoBehaviour
{
    [SerializeField] private Button[] buttons;

    private void Start()
    {
        buttons[0]?.Select();
    }
}
