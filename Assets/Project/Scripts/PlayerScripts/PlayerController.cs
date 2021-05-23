using Project.Classes.Damagable;
using UnityEngine;

namespace Project.Scripts.PlayerScripts
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

        public static PlayerController Instance { get; private set; }
        
        private void Start()
        {
            Instance = this;
            var player = Player.Instance;
            player.Human.UnityHumanData.Initialize(human, humanAnimator, humanCharacterController, humanGunTransform);
            player.Robot.UnityRobotData.Initialize(robot, robotAnimator, robotCharacterController, robotGunTransform);
        }
    }
}
