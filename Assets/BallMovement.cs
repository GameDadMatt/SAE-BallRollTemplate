using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallMovement : MonoBehaviour
{
    public float speed = 0;
    public bool canJump = false;
    public bool canDoubleJump = false;
    public bool canInfinitelyJump = false;
    public float jumpForce = 10;

    private Rigidbody rb;
    private float groundDistance;
    public bool hasDoubleJumped = false;

    private float movementX = 0;
    private float movementY = 0;
    private float movementZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameManagement.instance.RegisterPlayer(this);
        rb = GetComponent<Rigidbody>();
        groundDistance = GetComponent<SphereCollider>().bounds.extents.y;
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnJump(InputValue jumpValue)
    {
        if (IsGrounded && canJump)
        {
            hasDoubleJumped = false;
            movementZ = jumpForce;
        }
        else if (canDoubleJump && !hasDoubleJumped)
        {
            movementZ = jumpForce;
            hasDoubleJumped = true;
        }
        else if (canInfinitelyJump)
        {
            movementZ = jumpForce;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, movementZ, movementY);

        rb.AddForce(movement * speed);

        //Return MovementZ to 0
        movementZ = 0;
    }

    public void Respawn(Vector3 pos)
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.angularVelocity = new Vector3(0f, 0f, 0f);
        transform.position = pos;
    }

    private bool IsGrounded
    {
        get
        {
            return Physics.Raycast(transform.position, -Vector3.up, groundDistance + 0.1f);
        }
    }

}