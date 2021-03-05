using Classes;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform human;
        [SerializeField] private Transform robot;

        public static PlayerController playerController;
        private Player player;

        private void Awake()
        {
            playerController = this;
            var player = new Player(new Human(10, 4, human), new Robot(2, 4, robot));
        }
    }
}
