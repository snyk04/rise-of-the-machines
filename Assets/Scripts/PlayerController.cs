using UnityEngine;

public class PlayerController : MonoBehaviour {
    private static readonly Vector3 GUN_ROTATION_OFFSET = 90 * Vector3.up;
    private Rigidbody rigidbodyComponent;
    private Animator animator;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform gun;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool isGunRotate;

    public float speed;

    void Awake() {
        rigidbodyComponent = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        camera = camera ? camera : Camera.main;
    }

    void FixedUpdate() {
        MovePlayer(out var moveVector);
        TurnPlayer();
        Animate(moveVector.x, moveVector.z, false);
    }

    private void MovePlayer(out Vector3 moveVector) {
        moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        var desiredDirection = (camera.transform.forward * moveVector.z + camera.transform.right * moveVector.x).normalized;
        var moveDirection = transform.position + desiredDirection * (Time.deltaTime * speed);
        rigidbodyComponent.MovePosition(moveDirection);
    }

    private void TurnPlayer() {
        var ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit, whatIsGround)) {
            var playerToMouse = hit.point - transform.position;
            playerToMouse.y = 0f;
            playerToMouse.Normalize();
            rigidbodyComponent.MoveRotation(Quaternion.LookRotation(playerToMouse));
            if (isGunRotate) {
                gun.LookAt(hit.point);
                gun.localEulerAngles += GUN_ROTATION_OFFSET;

            }
        }
    }


    private void Animate(float hor, float ver, bool isDead) {
        AnimateMove(hor, ver);
        AnimateDeath(isDead);
    }

    private void AnimateMove(float hor, float ver) {
        animator.SetBool("IsWalking", !hor.Equals(0) && !ver.Equals(0));
        animator.SetFloat("Forward", ver);
        animator.SetFloat("Strafe", hor);
    }

    private void AnimateDeath(bool isDead) {
        animator.SetBool("IsDead", isDead);
    }
}