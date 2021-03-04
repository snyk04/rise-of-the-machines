using Classes;
using UnityEngine;

namespace PlayerScripts {
    public class PlayerController : MonoBehaviour {
        public static PlayerController playerController;

        [SerializeField] private Transform raycastTarget;
        private Player player;

        void Awake() {
            playerController = this;
            var player = new Player(new Human(10, 3), new Robot(2, 3));
        }
    }
}