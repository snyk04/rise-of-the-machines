using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigidbodyComponent;
    private Animator animator;

    public float speed;

    void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (!moveVector.Equals(Vector3.zero))
        {
            rigidbodyComponent.velocity = moveVector * speed;
            animator.SetBool("IsWalking", true);
            animator.SetFloat("Forward", 1);
            Vector2 xzPosition = new Vector2(moveVector.x, moveVector.z);
            transform.rotation = Quaternion.Euler(0, Vector2.SignedAngle(xzPosition, Vector2.up), 0);
        }
        else
        {
            animator.SetFloat("Forward", 0);
        }
    }
}
