using System.Collections;
using Cinemachine;
using Classes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerScripts
{
    public class PlayerChanger : MonoBehaviour
    {
        [SerializeField] private GameObject robot;
        [SerializeField] private GameObject human;
        [SerializeField] private GameObject emptyRobot;
        [Space]
        [SerializeField] private GameObject enterText;
        [SerializeField] private GameObject exitText;
        [Space]
        [SerializeField] private Transform humanLookAt;
        [SerializeField] private Transform robotLookAt;
        [SerializeField] private Transform humanFollow;
        [SerializeField] private Transform robotFollow;
        [Space]
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private AudioSource audioSource;
        [Space]
        [SerializeField] private AudioClip enterRobotSound;
        [SerializeField] private AudioClip exitRobotSound;
        [Space] 
        [SerializeField] private float humanSpawnDistance;

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
       
        private void EnterRobot()
        {
            Player.Instance.CurrentState = Player.State.Robot;
            emptyRobot.SetActive(false);
            enterText.SetActive(false);

            human.SetActive(false);
            robot.SetActive(true);
            SetVirtualCameraTarget(robotFollow, robotLookAt);

            ActivateExitText();
            PlaySound(enterRobotSound);
        }
        private void ExitRobot()
        {
            Player.Instance.CurrentState = Player.State.Human;
            exitText.SetActive(false);
            robot.SetActive(false);

            emptyRobot.transform.position = robot.transform.position;
            emptyRobot.transform.eulerAngles = robot.transform.eulerAngles;
            human.transform.position = robot.transform.position + robot.transform.forward * humanSpawnDistance;

            emptyRobot.SetActive(true);
            human.SetActive(true);
            SetVirtualCameraTarget(humanFollow, humanLookAt);
            PlaySound(exitRobotSound);
        }

        public void ActivateEnterText()
        {
            enterText.SetActive(true);
            StartCoroutine(WaitForEnter());
        }
        public void ActivateExitText()
        {
            exitText.SetActive(true);
            StartCoroutine(WaitForExit());
        }

        public void DeactivateEnterText()
        {
            enterText.SetActive(false);
            StopCoroutine(WaitForEnter());
        }
        public void DeactivateExitText()
        {
            enterText.SetActive(true);
            StopCoroutine(WaitForExit());
        }

        private IEnumerator WaitForEnter()
        {
            while (Player.Instance.CurrentState == Player.State.Human)
            {
                if (Keyboard.current[Key.G].wasPressedThisFrame)
                {
                    EnterRobot();
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
                    ExitRobot();
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
    }
}
