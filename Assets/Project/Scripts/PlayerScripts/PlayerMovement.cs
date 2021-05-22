using Project.Classes.Damagable;
using Project.Classes.Utils;
using UnityEngine;

namespace Project.Scripts.PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly Vector3 GUN_ROTATION_OFFSET = 90 * Vector3.up;

        // [SerializeField] private CharacterController characterController;
        [Header("Components")]
        [SerializeField] private PlayerChanger playerChanger;
        [SerializeField] private PlayerAnimation animationController;
        [SerializeField] private Camera mainCamera;

        [Header("Settings")]
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private bool isGunRotate;
        [SerializeField] private float rotationSensitivity;

        private Vector2 moveDirection;
        private Vector2 rotateDirection;

        private void Awake()
        {
            animationController = GetComponent<PlayerAnimation>();
            mainCamera = mainCamera ? mainCamera : Camera.main;
        }
        private void FixedUpdate()
        {
            MovePlayer(moveDirection, out var localMoveDir);
            TurnPlayer();
            animationController.Animate(localMoveDir.x, localMoveDir.y, false);
        }

        public void OnMoveInput(float x, float y)
        {
            moveDirection.x = x;
            moveDirection.y = y;
        }
        public void OnRotateInput(float x, float y)
        {
            rotateDirection.x = x;
            rotateDirection.y = y;
        }

        private void MovePlayer(Vector2 moveDir, out Vector2 localMoveDir)
        {
            var moveVector = new Vector3(moveDir.x, 0, moveDir.y);
            var desiredDirection =
                mainCamera.transform.forward * moveVector.z + mainCamera.transform.right * moveVector.x;
            desiredDirection.y = 0f;
            desiredDirection.Normalize();

            var moveToPosition = desiredDirection * (Time.deltaTime * Player.Instance.MoveSpeed.Value);
            Player.Instance.CharacterController.Move(moveToPosition);
            localMoveDir = new Vector2 { x = desiredDirection.x, y = desiredDirection.z }.RotateDegrees(-Player.Instance
                .Transform
                .rotation.eulerAngles.y);
        }
        private void TurnPlayer()
        {
            var ray = mainCamera.ScreenPointToRay(rotateDirection);

            if (Physics.Raycast(ray, out var hit, whatIsGround))
            {
                Vector3 currentRotateVector = Vector3.Lerp(Player.Instance.Transform.forward, hit.point - Player.Instance.Transform.position, Time.deltaTime * rotationSensitivity);
                Player.Instance.Transform.forward = currentRotateVector;
                Player.Instance.Transform.localEulerAngles = Vector3.up * Player.Instance.Transform.localEulerAngles.y;
                if (isGunRotate)
                {
                    // todo Clamp rotation of gun 
                    Player.Instance.GunTransform.LookAt(hit.point);
                    Player.Instance.GunTransform.localEulerAngles += GUN_ROTATION_OFFSET;
                }
            }
        }
    }
}
