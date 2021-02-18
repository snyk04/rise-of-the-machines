using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        private static readonly Vector3 GUN_ROTATION_OFFSET = 90 * Vector3.up;
        private Rigidbody rigidbodyComponent;
        private PlayerAnimationController animationController;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform gun;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private bool isGunRotate;

        public float speed;

        void Awake()
        {
            rigidbodyComponent = GetComponent<Rigidbody>();
            animationController = GetComponent<PlayerAnimationController>();
            mainCamera = mainCamera ? mainCamera : Camera.main;
        }
        void FixedUpdate()
        {
            MovePlayer(out var moveVector);
            TurnPlayer();
            animationController.Animate(moveVector.x, moveVector.z, false);
        }

        private void MovePlayer(out Vector3 moveVector)
        {
            moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            var desiredDirection = (mainCamera.transform.forward * moveVector.z + mainCamera.transform.right * moveVector.x).normalized;
            desiredDirection = new Vector3(desiredDirection.x, transform.position.y, desiredDirection.z);
            var moveDirection = transform.position + desiredDirection * (Time.deltaTime * speed);
            rigidbodyComponent.MovePosition(moveDirection);
        }
        private void TurnPlayer()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, whatIsGround))
            {
                var playerToMouse = hit.point - transform.position;
                playerToMouse.y = 0f;
                playerToMouse.Normalize();
                rigidbodyComponent.MoveRotation(Quaternion.LookRotation(playerToMouse));
                if (isGunRotate)
                {
                    gun.LookAt(hit.point);
                    gun.localEulerAngles += GUN_ROTATION_OFFSET;
                }
            }
        }
    }
}
