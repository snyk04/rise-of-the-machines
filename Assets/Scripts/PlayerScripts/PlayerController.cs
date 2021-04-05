using Classes;
using UnityEngine;

namespace PlayerScripts {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private Transform human;
        [SerializeField] private Transform robot;
        [SerializeField] private Animator humanAnimator;
        [SerializeField] private Animator robotAnimator;
        [SerializeField] private CharacterController humanCharacterController;
        [SerializeField] private CharacterController robotCharacterController;
        [SerializeField] private Transform humanGunTransform;
        [SerializeField] private Transform robotGunTransform;

        public static PlayerController playerController;
        private Player player;

        private void Awake() {
            playerController = this;
            var player = new Player(new Human(10, 4, 20, human, humanAnimator, humanCharacterController, humanGunTransform),
                                    new Robot(20, 4, 20, robot, robotAnimator, robotCharacterController, robotGunTransform)); // todo add HumanSO, RobotSO
        }
    }
}