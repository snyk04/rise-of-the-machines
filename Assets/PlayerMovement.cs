using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigidbodyComponent;

    public float speed;

    void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rigidbodyComponent.velocity = moveVector * speed;
        if (moveVector != Vector3.zero)
        {
            Vector2 xzPosition = new Vector2(moveVector.x, moveVector.z);
            transform.rotation = Quaternion.Euler(0, Vector2.SignedAngle(xzPosition, Vector2.up), 0);
        }
    }
}
