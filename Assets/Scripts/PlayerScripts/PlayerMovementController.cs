using System;
using Classes;
using UnityEngine;


namespace PlayerScripts {
    public class PlayerMovementController : MonoBehaviour { 
        public CharacterController characterController;
        public PlayerChanger playerChanger;
        private static readonly Vector3 GUN_ROTATION_OFFSET = 90 * Vector3.up;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Transform gun;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private bool isGunRotate;

        public float speed;

        void Awake() {
            animationController = GetComponent<PlayerAnimationController>();
            mainCamera = mainCamera ? mainCamera : Camera.main;
        }

        void FixedUpdate() {
            MovePlayer(out var localMoveDir);
            TurnPlayer();
            animationController.Animate(localMoveDir.x, localMoveDir.y, false);
        }

        private void MovePlayer(out Vector2 localMoveDir) {
            var moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            var desiredDirection =
                mainCamera.transform.forward * moveVector.z + mainCamera.transform.right * moveVector.x;
            desiredDirection.y = 0f;
            desiredDirection.Normalize();
            var moveToPosition = desiredDirection * (Time.deltaTime * speed);
            characterController.Move(moveToPosition);
            localMoveDir = new Vector2 {x = desiredDirection.x, y = desiredDirection.z}.RotateDegrees(-transform.rotation.eulerAngles.y);
        }

        private void TurnPlayer() {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, whatIsGround)) {
                transform.LookAt(hit.point);
                transform.localEulerAngles = Vector3.up * transform.localEulerAngles.y;
                if (isGunRotate) { // todo Clamp rotation of gun 
                    gun.LookAt(hit.point);
                    gun.localEulerAngles += GUN_ROTATION_OFFSET;
                }
            }
        }
    }
}