using Classes;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController playerController;

        private Player player;

        private void Awake()
        {
            playerController = this;
            var player = new Player(new Human(10, 3), new Robot(2, 3));
        }
    }
}
