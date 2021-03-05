using Classes;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMovementController : MonoBehaviour
    {
        private static readonly Vector3 GUN_ROTATION_OFFSET = 90 * Vector3.up;

        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform gun;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private bool isGunRotate;

        private Rigidbody rigidbodyComponent;
        private PlayerAnimation animationController;

        public float speed;

        private void Awake()
        {
            rigidbodyComponent = GetComponent<Rigidbody>();
            animationController = GetComponent<PlayerAnimation>();
            mainCamera = mainCamera ? mainCamera : Camera.main;
        }
        private void FixedUpdate()
        {
            MovePlayer(out Vector2 localMoveDir);
            TurnPlayer();
            animationController.Animate(localMoveDir.x, localMoveDir.y, false);
        }

        private void MovePlayer(out Vector2 localMoveDir)
        {
            var moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            Vector3 desiredDirection = 
                mainCamera.transform.forward * moveVector.z + 
                mainCamera.transform.right * moveVector.x;
            desiredDirection.y = 0f;
            desiredDirection.Normalize();

            Vector3 goalPosition =
                transform.position + 
                desiredDirection * (Time.deltaTime * Player.player.MoveSpeed);
            rigidbodyComponent.MovePosition(goalPosition);

            localMoveDir = new Vector2
            {
                x = desiredDirection.x,
                y = desiredDirection.z
            }.RotateDegrees(-transform.rotation.eulerAngles.y);
        }
        private void TurnPlayer()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, whatIsGround))
            {
                Vector3 playerToMouse = hit.point - transform.position;
                playerToMouse.y = 0f;
                playerToMouse.Normalize();
                rigidbodyComponent.MoveRotation(Quaternion.LookRotation(playerToMouse));
                if (isGunRotate)
                { 
                    // todo Clamp rotation of gun 
                    gun.LookAt(hit.point);
                    gun.localEulerAngles += GUN_ROTATION_OFFSET;
                }
            }
        }
    }
}
