using System.Collections;
using Cinemachine;
using Classes;
using UnityEngine;

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
        [Space]
        [SerializeField] private float humanSpawnDistance;

        private void SetVirtualCameraTarget(Transform follow, Transform lookAt)
        {
            virtualCamera.LookAt = lookAt;
            virtualCamera.Follow = follow;
        }

        private void ExitRobot()
        {
            Player.player.CurrentState = Player.State.Human;
            exitText.SetActive(false);
            robot.SetActive(false);

            emptyRobot.transform.position = robot.transform.position;
            emptyRobot.transform.eulerAngles = robot.transform.eulerAngles;
            human.transform.position = robot.transform.position + robot.transform.forward * humanSpawnDistance;

            emptyRobot.SetActive(true);
            human.SetActive(true);
            SetVirtualCameraTarget(humanFollow, humanLookAt);

        }
        private void EnterRobot()
        {
            Player.player.CurrentState = Player.State.Robot;
            emptyRobot.SetActive(false);
            enterText.SetActive(false);

            human.SetActive(false);
            robot.SetActive(true);
            SetVirtualCameraTarget(robotFollow, robotLookAt);

            ActivateExit();
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
            while (Player.player.CurrentState == Player.State.Human)
            {
                if (Input.GetKeyDown(KeyCode.G))
                {
                    EnterRobot();
                }
                yield return null;
            }
        }
        private IEnumerator WaitForExit()
        {
            while (Player.player.CurrentState == Player.State.Robot)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ExitRobot();
                }
                yield return null;
            }
        }
    }
}
