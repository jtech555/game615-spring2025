using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 30f;          // Movement speed when pressing directional buttons
    public float drag = 0.99f;
    public float normalFrictionCoefficient = 5f; // Strength of friction when grounded
    public float maxSpeed = 15f;           // Maximum velocity the character can reach
    public float maxForce = 25f;           // Maximum total acceleration applied
    public float jumpForce = 8f;           // Force applied for jumping

    private float lastGroundedTime = 0f;  // Time when the player was last grounded
    public float coyoteTimeDuration = 2f; // Time window for coyote jump

    [SerializeField]
    public CharacterController cc;         // Reference to CharacterController

    [SerializeField]
    Camera cam;


    [SerializeField]
    AnimationCurve jumpForceCurve;

    private Vector3 velocity;              // Current velocity (includes all forces)
    private Vector3 acceleration;          // Accumulated forces (gravity, movement, friction)
    
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        // Rotate character based on input
        // transform.Rotate(0, hAxis * 60 * Time.deltaTime, 0);

        // Gravity handling (always apply gravity, even if grounded)
        acceleration = new Vector3(0, -9.81f, 0);

        // Apply movement based on the input and camera direction
        Vector3 cameraForward = cam.gameObject.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        acceleration += cameraForward * vAxis * moveSpeed;

        Vector3 cameraRight = cam.gameObject.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();
        acceleration += cameraRight * hAxis * moveSpeed;

        // Apply friction as an acceleration (opposite direction of horizontal velocity)
        if (hAxis == 0 && vAxis == 0) {
            ApplyFriction(normalFrictionCoefficient);
        }


        bool isWallSliding = false;
        Vector3 wallNormal = Vector3.zero;
        if (!cc.isGrounded)
        {
            if (IsTouchingWall(out wallNormal))
            {
                isWallSliding = true;
                velocity.y = Mathf.Max(velocity.y, -2f); // Limit downward speed on walls
            }
        }

        // Track when the player was last on the ground
        if (cc.isGrounded)
        {
            lastGroundedTime = Time.time; // Update last grounded time
        }
        bool canJump = (cc.isGrounded || Time.time - lastGroundedTime < coyoteTimeDuration);
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;
            lastGroundedTime = 0f; // Reset coyote time after jumping
        }

        if (isWallSliding)
        {   
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity = wallNormal * 10f + Vector3.up * jumpForce; // Push off the wall
            }
        }

        // Clamp acceleration to avoid excessive values
        acceleration = Vector3.ClampMagnitude(acceleration, maxForce);

        // Apply the acceleration to velocity
        velocity += acceleration * Time.deltaTime;

        // Apply maximum speed limit to velocity
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Move the character
        cc.Move(velocity * Time.deltaTime);

        Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
        if (horizontalVelocity.magnitude > 0.1f) {
            transform.forward = horizontalVelocity.normalized;
        }

        // Reset acceleration for the next frame (don't carry over from previous frame)
        acceleration = Vector3.zero;
    }

    void ApplyFriction(float frictionCoefficient)
    {
        Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);

        if (horizontalVelocity.magnitude > 0.01f) // Only apply friction if there's movement
        {
            // Friction force: proportional to velocity, clamped to avoid oscillation
            float frictionForce = frictionCoefficient * Time.deltaTime;
            Vector3 friction = horizontalVelocity.normalized * frictionForce;

            // Prevent overshooting (stop completely if friction would reverse velocity)
            if (friction.magnitude > horizontalVelocity.magnitude)
            {
                velocity.x = 0;
                velocity.z = 0;
            }
            else
            {
                velocity -= friction;
            }
        }
    }

    bool IsTouchingWall(out Vector3 wallNormal)
    {
        RaycastHit hit;
        float wallCheckDistance = 0.6f;
        wallNormal = Vector3.zero;
        LayerMask mask = LayerMask.GetMask("wall");

        if (Physics.Raycast(transform.position, transform.forward, out hit, wallCheckDistance, mask))
        {
            wallNormal = hit.normal;
            return true;
        }

        return false;
    }
}
