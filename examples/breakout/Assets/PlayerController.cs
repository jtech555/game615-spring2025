using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveSpeed = 30f;          // Movement speed when pressing directional buttons
    float normalFrictionCoefficient = 15f; // Strength of friction when grounded
    float maxSpeed = 15f;           // Maximum velocity the character can reach
    float maxForce = 25f;           // Maximum total acceleration applied
    float jumpForce = 8f;           // Force applied for jumping

    private float lastGroundedTime = 0f;  // Time when the player was last grounded
    float coyoteTimeDuration = 2f; // Time window for coyote jump

    [SerializeField]
    public CharacterController cc;         // Reference to CharacterController

    [SerializeField]
    Camera cam;

    private Vector3 velocity;              // Current velocity (includes all forces)
    private Vector3 acceleration;          // Accumulated forces (gravity, movement, friction)
    
    private bool isOnPlatform = false;
    private MovingPlatform currentPlatform;
    private Vector3 previousPlatformPosition;

    public void SetOnPlatform(bool onPlatform, MovingPlatform platform)
    {
        isOnPlatform = onPlatform;
        currentPlatform = platform;
        if (onPlatform) { 
            previousPlatformPosition = platform.transform.position;
        }
    }

    void Update()
    {
        // DEAL WITH PLATFORMS
        Vector3 platformMovement = Vector3.zero;
        if (isOnPlatform && currentPlatform != null)
        {
            // Get the platform's previous position
            platformMovement = currentPlatform.transform.position - previousPlatformPosition; 

            // Apply the platform's movement to the player's velocity
            // velocity += platformMovement / Time.deltaTime; // Adjust the player's velocity based on the platform's movement

            previousPlatformPosition = currentPlatform.transform.position;
        }


        // DEAL WITH WALLS
        Vector3 wallNormal = Vector3.zero;
        bool touchingWall = IsTouchingWall(out wallNormal);
        if (touchingWall) {
            float dotProduct = Vector3.Dot(velocity.normalized, -wallNormal);
            float slowdownFactor = Mathf.Clamp01(1 - dotProduct); // Calculate slowdown based on angle of collision

            // Slow down the player's velocity based on the angle of collision
            velocity.x *= slowdownFactor;
            velocity.z *= slowdownFactor;
            velocity.y = Mathf.Max(velocity.y, -2f); // Limit downward speed on walls
        }


        // NORMAL GRAVITY
        // Gravity handling (always apply gravity, even if grounded)
        acceleration = new Vector3(0, -9.81f, 0);

        // INPUT
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        // Apply movement based on the input and camera direction
        Vector3 cameraForward = cam.gameObject.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        acceleration += cameraForward * vAxis * moveSpeed;
        Vector3 cameraRight = cam.gameObject.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();
        acceleration += cameraRight * hAxis * moveSpeed;


        // APPLY FRICTION
        // Apply friction as an acceleration (opposite direction of horizontal velocity)
        if (hAxis == 0 && vAxis == 0) {
            ApplyFriction(normalFrictionCoefficient);
        }

        // JUMP
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
        // Allow falling when jump is released
        if (Input.GetKeyUp(KeyCode.Space) && velocity.y > 0)
        {
            velocity.y *= 0.5f; // Reduce upward velocity to start falling
        }
        // WALL SLIDING
        if (!cc.isGrounded && touchingWall)
        {   
            velocity.y = Mathf.Max(velocity.y, -2f); // Limit downward speed while wall sliding
            // velocity += wallNormal * wallSlideSpeed * Time.deltaTime; // Slide down the wall
            // WALL JUMP
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity = wallNormal * 3 + Vector3.up * jumpForce; // Push off the wall
            }
        }

        // APPLY PHYSICS
        // Clamp acceleration to avoid excessive values
        acceleration = Vector3.ClampMagnitude(acceleration, maxForce);
        // Apply the acceleration to velocity
        velocity += acceleration * Time.deltaTime;
        // Apply maximum speed limit to velocity
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Move the character
        cc.Move(velocity * Time.deltaTime + platformMovement);

        // ROTATE THE PLAYER IN THE DIRECTION OF MOVEMENT
        Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
        if (horizontalVelocity.magnitude > 0.1f) {
            transform.forward = horizontalVelocity.normalized;
        }
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
