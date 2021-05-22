using UnityEngine;

namespace Project.Scripts.UserInterface
{
    public class MinimapController : MonoBehaviour
    {
        [SerializeField] private GameObject human;
        [SerializeField] private GameObject robot;
        [SerializeField] private GameObject camera;

        private Vector3 defaultOffset = new Vector3(0, 30, 0);

        void Update()
        {
            if (human.activeInHierarchy == true)
            {
                camera.transform.position = human.transform.position + defaultOffset;
            }
            else
            {
                camera.transform.position = robot.transform.position + defaultOffset;
            }
        }
    }
}
