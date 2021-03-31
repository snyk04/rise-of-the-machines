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
        [SerializeField] private GameObject enterText;
        [SerializeField] private GameObject exitText;
        [SerializeField] private Transform humanLookAt;
        [SerializeField] private Transform robotLookAt;
        [SerializeField] private Transform humanFollow;
        [SerializeField] private Transform robotFollow;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private AudioClip robotEnterSnd;
        [SerializeField] private AudioClip robotExitSnd;
        [SerializeField] private GameObject soundSource;
        [Space] [SerializeField] private float humanSpawnDistance;

        private void SetVirtualCameraTarget(Transform follow, Transform lookAt)
        {
            virtualCamera.LookAt = lookAt;
            virtualCamera.Follow = follow;
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
            StartCoroutine(exitSound());
        }

        private void EnterRobot()
        {
            Player.Instance.CurrentState = Player.State.Robot;
            emptyRobot.SetActive(false);
            enterText.SetActive(false);

            human.SetActive(false);
            robot.SetActive(true);
            SetVirtualCameraTarget(robotFollow, robotLookAt);

            ActivateExit();
            StartCoroutine(enterSound());
        }

        public void ActivateEnter()
        {
            enterText.SetActive(true);
            StartCoroutine(WaitForEnter());
        }

        public void ActivateExit()
        {
            exitText.SetActive(true);
            StartCoroutine(WaitForExit());
        }

        public void DeactivateEnter()
        {
            enterText.SetActive(false);
            StopCoroutine(WaitForEnter());
        }

        public void DeactivateExit()
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

        IEnumerator enterSound()
        {
            var source = soundSource.AddComponent<AudioSource>();

            source.clip = robotEnterSnd;
            source.minDistance = 1;
            source.maxDistance = 50;
            source.volume = 1f;
            source.spatialBlend = 1f;
            source.Play();

            yield return new WaitForSeconds(source.clip.length);
            Destroy(source);
        }

        IEnumerator exitSound()
        {
            var source = soundSource.AddComponent<AudioSource>();

            source.clip = robotExitSnd;
            source.minDistance = 1;
            source.maxDistance = 50;
            source.volume = 1f;
            source.spatialBlend = 1f;
            source.Play();

            yield return new WaitForSeconds(source.clip.length);
            Destroy(source);
        }
    }
}