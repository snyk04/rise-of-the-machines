using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody rigidbodyComponent;
    private Animator animator;

    public float speed;

    void Awake() {
        rigidbodyComponent = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        var moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        var desiredDirection = transform.forward * moveVector.z + transform.right * moveVector.x;
        var moveDirection = transform.position + desiredDirection * (Time.deltaTime * speed);
        rigidbodyComponent.MovePosition(moveDirection);
        // var xzPosition = new Vector2(moveVector.x, moveVector.z);
        // transform.rotation = Quaternion.Euler(0, Vector2.SignedAngle(xzPosition, Vector2.up), 0);
        Animate(moveVector.x, moveVector.z);
    }

    private void Animate(float hor, float ver) {
        AnimateMove(hor, ver);
    }

    private void AnimateMove(float hor, float ver) {
        animator.SetBool("IsWalking", true);
        animator.SetFloat("Forward", ver);
        animator.SetFloat("Strafe", hor);
    }
}