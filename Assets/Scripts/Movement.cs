using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 10f;
    public LayerMask mask;
    public Transform groundCheck;
    public float jumpForce = 10f;
    public float airControlFactor = 0.5f;
    public float maxGroundCheckDistance = 0.3f;
    public float groundDrag = 6f;
    public float airDrag = 1f;
    private bool isGrounded;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
        rb.freezeRotation = true;
    }

    void Update()
    {
        CheckGround();
        HandleMovement();
        HandleJump();
        ApplyDrag();
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, maxGroundCheckDistance, mask);
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 movement = Vector3.right * horizontalInput * moveSpeed;

        if (isGrounded)
        {
            // On ground, move based on velocity directly
            rb.velocity = new Vector3(movement.x, rb.velocity.y, 0f);
        }
        else
        {
            // In air, reduce control
            rb.velocity += Vector3.right * horizontalInput * (moveSpeed * airControlFactor) * Time.deltaTime;
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
        }
    }

    void ApplyDrag()
    {
        rb.drag = isGrounded ? groundDrag : airDrag;
    }
}
