using System.Collections;
using Cinemachine;
using Classes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class PlayerChanger : MonoBehaviour
    {
        #region Properties

        [Header("Gameobjects and transforms")]
        [SerializeField] private GameObject robot;
        [SerializeField] private GameObject human;
        [SerializeField] private GameObject emptyRobot;
        [Space]
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

        #endregion

        private void Start()
        {
            audioSource.minDistance = 1;
            audioSource.maxDistance = 50;
            audioSource.volume = 1f;
            audioSource.spatialBlend = 1f;
        }

        private void SetVirtualCameraTarget(Transform follow, Transform lookAt)
        {
            virtualCamera.LookAt = lookAt;
            virtualCamera.Follow = follow;
        }

        private IEnumerator EnterRobot()
        {
            // TODO: блокировать управление
            yield return StartCoroutine(canvasController.FadeScreen());

            PlaySound(enterRobotSound);

            string[] textQuery = { "Одеваемся.", "Одеваемся..", "Одеваемся..." };
            canvasController.StartAnimation(textQuery);

            ChangePlayerState(Player.State.Robot);
            emptyRobot.SetActive(false);
            human.SetActive(false);
            robot.SetActive(true);
            SetVirtualCameraTarget(robotFollow, robotLookAt);
            DeactivateEnterText();
            ActivateExitText();

            yield return new WaitForSeconds(enterRobotSound.length);
            canvasController.StopAnimation();

            // TODO: разблокировать управление
            yield return StartCoroutine(canvasController.UnfadeScreen());
        }
        private IEnumerator ExitRobot()
        {
            // TODO: блокировать управление
            yield return StartCoroutine(canvasController.FadeScreen());

            PlaySound(exitRobotSound);

            string[] textQuery = { "Раздеваемся.", "Раздеваемся..", "Раздеваемся..." };
            canvasController.StartAnimation(textQuery);

            ChangePlayerState(Player.State.Human);
            emptyRobot.SetActive(true);
            human.SetActive(true);
            robot.SetActive(false);
            emptyRobot.transform.position = robot.transform.position;
            emptyRobot.transform.eulerAngles = robot.transform.eulerAngles;
            human.transform.position = robot.transform.position + robot.transform.forward * humanSpawnDistance;
            SetVirtualCameraTarget(humanFollow, humanLookAt);
            DeactivateExitText();

            yield return new WaitForSeconds(exitRobotSound.length);
            canvasController.StopAnimation();

            // TODO: разблокировать управление
            yield return StartCoroutine(canvasController.UnfadeScreen());
        }

        public void ActivateEnterText()
        {
            canvasController.EnterText.SetActive(true);
            StartCoroutine(WaitForEnter());
        }
        public void ActivateExitText()
        {
            canvasController.ExitText.SetActive(true);
            StartCoroutine(WaitForExit());
        }
        public void DeactivateEnterText()
        {
            canvasController.EnterText.SetActive(false);
            StopCoroutine(WaitForEnter());
        }
        public void DeactivateExitText()
        {
            canvasController.ExitText.SetActive(false);
            StopCoroutine(WaitForExit());
        }

        // TODO: Заменить это на ивентики там хз, ну без корутин чтобы было, по православному чтобы
        private IEnumerator WaitForEnter()
        {
            while (Player.Instance.CurrentState == Player.State.Human)
            {
                if (Keyboard.current[Key.G].wasPressedThisFrame)
                {
                    StartCoroutine(EnterRobot());
                }

                yield return null;
            }
        }
        private IEnumerator WaitForExit()
        {
            while (Player.Instance.CurrentState == Player.State.Robot)
            {
                if (Keyboard.current[Key.F].wasPressedThisFrame)
                {
                    StartCoroutine(ExitRobot());
                }

                yield return null;
            }
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
    }
}
