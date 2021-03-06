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
        [SerializeField] private GameObject robotTrigger;
        [SerializeField] private GameObject robotModel;
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

            robotModel.transform.position = robot.transform.position;
            robotModel.transform.eulerAngles = robot.transform.eulerAngles;
            human.transform.position = robot.transform.position + robot.transform.forward * humanSpawnDistance;
            robotModel.SetActive(true);
            human.SetActive(true);
        }
        private void EnterRobot()
        {
            currentState = State.Robot;
            robotModel.SetActive(false);
            enterText.SetActive(false);
            human.SetActive(false);
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
