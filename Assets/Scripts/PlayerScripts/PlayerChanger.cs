using System;
using System.Collections;
using Cinemachine;
using Classes;
using InputHandling;
using UnityEngine;
using UnityEngine.InputSystem;
using UserInterface;

namespace PlayerScripts
{
    public class PlayerChanger : MonoBehaviour
    {
        #region Properties

        [Header("Gameobjects")]
        [SerializeField] private GameObject robot;
        [SerializeField] private GameObject human;
        [SerializeField] private GameObject emptyRobot;

        [Header("Cinemachine points")]
        [SerializeField] private Transform humanLookAt;
        [SerializeField] private Transform robotLookAt;
        [SerializeField] private Transform humanFollow;
        [SerializeField] private Transform robotFollow;
        
        [Header("Components")]
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private CanvasController canvasController;
        
        [Header("Sound")]
        [SerializeField] private AudioClip enterRobotSound;
        [SerializeField] private AudioClip exitRobotSound;
        
        [Header("Settings")]
        [SerializeField] private float humanSpawnDistance;

        private InputCombat combatInput;
        private InputInteraction interactionInput;
        private InputMovement movementInput;

        private Action<InputAction.CallbackContext> enterRobotAction;
        private Action<InputAction.CallbackContext> exitRobotAction;

        #endregion

        #region Behaviour methods
        
        private void Start()
        {
            combatInput = InputCombat.Instance;
            interactionInput = InputInteraction.Instance;
            movementInput = InputMovement.Instance;

            audioSource.minDistance = 1;
            audioSource.maxDistance = 50;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 1f;
        }

        #endregion

        #region Methods

        private void SetVirtualCameraTarget(Transform follow, Transform lookAt)
        {
            virtualCamera.LookAt = lookAt;
            virtualCamera.Follow = follow;
        }

        private IEnumerator EnterRobotCoroutine()
        {
            DisableControls();
            DeactivateEnterText();

            yield return StartCoroutine(canvasController.FadeScreen());

            PlaySound(enterRobotSound);

            string[] textQuery = { "Одеваемся.", "Одеваемся..", "Одеваемся..." };
            canvasController.StartAnimation(textQuery);

            emptyRobot.SetActive(false);

            SetVirtualCameraTarget(robotFollow, robotLookAt);

            yield return new WaitForSeconds(enterRobotSound.length);
            canvasController.StopAnimation();
            
            human.SetActive(false);
            robot.SetActive(true);
            
            ChangePlayerState(Player.State.Robot);

            yield return StartCoroutine(canvasController.UnfadeScreen());

            EnableControls();
            ActivateExitText();
        }
        private IEnumerator ExitRobotCoroutine()
        {
            DisableControls();
            DeactivateExitText();

            yield return StartCoroutine(canvasController.FadeScreen());

            PlaySound(exitRobotSound);

            string[] textQuery = { "Раздеваемся.", "Раздеваемся..", "Раздеваемся..." };
            canvasController.StartAnimation(textQuery);

            emptyRobot.SetActive(true);
            emptyRobot.transform.position = robot.transform.position;
            emptyRobot.transform.eulerAngles = robot.transform.eulerAngles;
            human.transform.position = robot.transform.position + robot.transform.forward * humanSpawnDistance;
            SetVirtualCameraTarget(humanFollow, humanLookAt);

            yield return new WaitForSeconds(exitRobotSound.length);
            canvasController.StopAnimation();
            
            human.SetActive(true);
            robot.SetActive(false);
            ChangePlayerState(Player.State.Human);

            yield return StartCoroutine(canvasController.UnfadeScreen());

            EnableControls();
        }

        private void EnterRobot()
        {
            StartCoroutine(EnterRobotCoroutine());
        }
        private void ExitRobot()
        {
            StartCoroutine(ExitRobotCoroutine());
        }

        public void ActivateEnterText()
        {
            canvasController.EnterText.SetActive(true);
            enterRobotAction = (InputAction.CallbackContext ctx) => EnterRobot();
            interactionInput.interactionActions.ChangeState.performed += enterRobotAction;
        }
        public void ActivateExitText()
        {
            canvasController.ExitText.SetActive(true);
            exitRobotAction = (InputAction.CallbackContext ctx) => ExitRobot();
            interactionInput.interactionActions.ChangeState.performed += exitRobotAction;
        }
        public void DeactivateEnterText()
        {
            canvasController.EnterText.SetActive(false);
            interactionInput.interactionActions.ChangeState.performed -= enterRobotAction;
        }
        public void DeactivateExitText()
        {
            canvasController.ExitText.SetActive(false);
            interactionInput.interactionActions.ChangeState.performed -= exitRobotAction;
        }

        private void PlaySound(AudioClip clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
        private void ChangePlayerState(Player.State state)
        {
            Player.Instance.CurrentState = state;
        }
        private void EnableControls()
        {
            combatInput.EnableControls();
            interactionInput.EnableControls();
            movementInput.EnableControls();
        }
        private void DisableControls()
        {
            combatInput.DisableControls();
            interactionInput.DisableControls();
            movementInput.DisableControls();
        }

        #endregion
    }
}
