using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform playerOrientation;

    [Header("Movement")]
    public float movementSpeed = 7f;
    float movementCohersion = 10f; // Help movement
    [SerializeField] float airMoveDampener = 0.4f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Jump")]
    public float jumpForce = 20f;

    float LHMovement; // Local Horizon
    float LVMovement; // Local Vertical
    float groundFriction = 6f;
    float airDragCoeff = 4f;
    
    Vector3 direction;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float groundDistance = 0.4f;


    Rigidbody playerBody;

    
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        playerBody.freezeRotation = true;
    }

    
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundDistance, groundMask);

        MovementInput();
        DragControl();

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
    }

    // Has the frequency of the physics system; it is called every fixed frame-rate frame.
    // Perfect for dealing with rigidbody FPController
    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovementInput()
    {
        LHMovement = Input.GetAxisRaw("Horizontal");
        LVMovement = Input.GetAxisRaw("Vertical");

        direction = playerOrientation.forward * LVMovement + playerOrientation.right * LHMovement;
    }

    void Jump()
    {
        playerBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void DragControl()
    {
        if (isGrounded)
        {
            playerBody.drag = groundFriction;
            return;
        }
        playerBody.drag = airDragCoeff;
    }

    void MovePlayer()
    {
        // Move according to ground friction
        if (isGrounded)
            playerBody.AddForce(direction.normalized * movementSpeed * movementCohersion, ForceMode.Acceleration);
        else // Move according to air drag
            playerBody.AddForce(direction.normalized * movementSpeed * movementCohersion * airMoveDampener, ForceMode.Acceleration);
    }

   
}
