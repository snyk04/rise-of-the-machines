using System.Collections;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerChanger : MonoBehaviour
    {
        private enum State
        {
            Human,
            Robot
        }

        [SerializeField] private GameObject robot;
        [SerializeField] private GameObject human;
        [SerializeField] private GameObject emptyRobot;
        [SerializeField] private GameObject enterText;
        [SerializeField] private GameObject exitText;
        [Space]
        [SerializeField] private float humanSpawnDistance;
        [SerializeField] private int keyDownChecksPerSecond;

        private State currentState;

        private void ExitRobot()
        {
            currentState = State.Human;
            exitText.SetActive(false);
            robot.SetActive(false);

            emptyRobot.transform.position = robot.transform.position;
            emptyRobot.transform.eulerAngles = robot.transform.eulerAngles;
            human.transform.position = robot.transform.position + robot.transform.forward * humanSpawnDistance;

            emptyRobot.SetActive(true);
            human.SetActive(true);
        }
        private void EnterRobot()
        {
            currentState = State.Robot;
            emptyRobot.SetActive(false);
            enterText.SetActive(false);
            human.SetActive(false);

            robot.transform.position = new Vector3(robot.transform.position.x, 0, robot.transform.position.z);

            robot.SetActive(true);

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
            while (currentState == State.Human)
            {
                yield return new WaitForSeconds(1 / keyDownChecksPerSecond);
                if (Input.GetKeyDown(KeyCode.G))
                {
                    EnterRobot();
                }
            }
        }
        private IEnumerator WaitForExit()
        {
            while (currentState == State.Robot)
            {
                yield return new WaitForSeconds(1 / keyDownChecksPerSecond);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ExitRobot();
                }
            }
        }
    }
}
