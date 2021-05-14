using Classes;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform human;
        [SerializeField] private Transform robot;
        [SerializeField] private Animator humanAnimator;
        [SerializeField] private Animator robotAnimator;
        [SerializeField] private CharacterController humanCharacterController;
        [SerializeField] private CharacterController robotCharacterController;
        [SerializeField] private Transform humanGunTransform;
        [SerializeField] private Transform robotGunTransform;

        public static PlayerController Instance;
        
        private Player player;

        private void Awake()
        {
            Instance = this;
            player = new Player(new Human(100, 4, 20, human, humanAnimator, humanCharacterController, humanGunTransform),
                                new Robot(100, 4, 20, robot, robotAnimator, robotCharacterController, robotGunTransform)); // todo add HumanSO, RobotSO
        }

        // private void Update() {
        //     Debug.Log(Player.Instance.Health.HP);   
        // }
    }
}
