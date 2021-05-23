using System;
using System.Collections;
using Project.Scripts.Characters;
using Project.Scripts.InputHandling;
using UnityEngine;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;
using Input = UnityEngine.Input;

namespace Project.Scripts.UserInterface
{
    public class CanvasController : MonoBehaviour
    {
        private enum State
        {
            Game,
            Inventory
        }
        
        #region Properties

        [Header("UI elements")]
        [SerializeField] private GameObject enterText;
        [SerializeField] private GameObject exitText;
        [SerializeField] private Image cover;
        [SerializeField] private Text changeStateText;
        [SerializeField] private GameObject main; 
        [SerializeField] private GameObject inventory; 

        [Header("Settings")]
        [SerializeField] private float fadeSensitivity;
        [SerializeField] private float animationDelay;

        public GameObject EnterText => enterText;
        public GameObject ExitText => exitText;
        public Image Cover => cover;
        public Text ChangeStateText => changeStateText;

        private Coroutine animationCoroutine;
        private State currentState;

        #endregion

        private void Awake()
        {
            currentState = State.Game;
        }
        private void Start()
        {
            var input = InputUserInterface.Instance;

            input.userInterfaceActions.Inventory.performed += context => InteractInventory();
        }

        public IEnumerator FadeScreen()
        {
            cover.gameObject.SetActive(true);

            float transparency = 0;
            while (transparency < 1)
            {
                transparency += 0.01f * fadeSensitivity;
                cover.color = new Color(0, 0, 0, transparency);
                yield return new WaitForEndOfFrame();
            }
        }
        public IEnumerator UnfadeScreen()
        {
            float transparency = 1;
            while (transparency > 0)
            {
                transparency -= 0.01f * fadeSensitivity;
                cover.color = new Color(0, 0, 0, transparency);
                yield return new WaitForEndOfFrame();
            }

            cover.gameObject.SetActive(false);
        }

        public void StartAnimation(string[] textQuery)
        {
            changeStateText.gameObject.SetActive(true);
            animationCoroutine = StartCoroutine(AnimateChangeStateText(textQuery));
        }
        public void StopAnimation()
        {
            StopCoroutine(animationCoroutine);
            changeStateText.text = "";
            changeStateText.gameObject.SetActive(false);
        }

        private IEnumerator AnimateChangeStateText(string[] textQuery)
        {
            int state = 0;
            while (true)
            {
                changeStateText.text = textQuery[state % textQuery.Length];
                state += 1;
                yield return new WaitForSeconds(animationDelay);
            }
        }

        private void InteractInventory()
        {
            if (currentState == State.Inventory)
            {
                InputCombat.Instance.EnableControls();
                InputInteraction.Instance.EnableControls();
                InputMovement.Instance.EnableControls();
                
                currentState = State.Game;
                inventory.SetActive(false);
                main.SetActive(true);
            }
            else
            {
                InputCombat.Instance.DisableControls();
                InputInteraction.Instance.DisableControls();
                InputMovement.Instance.DisableControls();
                
                currentState = State.Inventory;
                main.SetActive(false);
                inventory.SetActive(true);
            }
        }
    }
}
