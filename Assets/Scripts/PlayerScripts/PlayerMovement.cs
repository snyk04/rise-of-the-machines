﻿using Classes;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly Vector3 GUN_ROTATION_OFFSET = 90 * Vector3.up;

        // [SerializeField] private CharacterController characterController;
        [SerializeField] private PlayerChanger playerChanger;
        [SerializeField] private PlayerAnimation animationController;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private bool isGunRotate;

        void Awake()
        {
            animationController = GetComponent<PlayerAnimation>();
            mainCamera = mainCamera ? mainCamera : Camera.main;
        }
        void FixedUpdate()
        {
            MovePlayer(out var localMoveDir);
            TurnPlayer();
            animationController.Animate(localMoveDir.x, localMoveDir.y, false);
        }

        private void MovePlayer(out Vector2 localMoveDir)
        {
            var moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            var desiredDirection =
                mainCamera.transform.forward * moveVector.z + mainCamera.transform.right * moveVector.x;
            desiredDirection.y = 0f;
            desiredDirection.Normalize();
            var moveToPosition = desiredDirection * (Time.deltaTime * Player.player.MoveSpeed);
            Player.player.CharacterController.Move(moveToPosition);
            localMoveDir = new Vector2 { x = desiredDirection.x, y = desiredDirection.z }.RotateDegrees(-Player.player.Transform.rotation.eulerAngles.y);
        }
        private void TurnPlayer()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, whatIsGround))
            {
                Player.player.Transform.LookAt(hit.point);
                Player.player.Transform.localEulerAngles = Vector3.up * Player.player.Transform.localEulerAngles.y;
                if (isGunRotate)
                { // todo Clamp rotation of gun 
                    Player.player.GunTransform.LookAt(hit.point);
                    Player.player.GunTransform.localEulerAngles += GUN_ROTATION_OFFSET;
                }
            }
        }
    }
}
