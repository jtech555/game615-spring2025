using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject cam;
    CharacterController cc;
    Vector3 velocity = Vector3.zero;
    float yVelocity = 0;
    float moveSpeed = 12;
    float jumpForce = 8;  // Increased jump force for better jump height
    float gravity = -19.81f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get player input for movement
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        // Reset velocity, note we are using a separate float for the yVelocity that 
        // IS NOT reset, and in fact is added to this velocity vector's y value every
        // frame.
        velocity = Vector3.zero;

        // Get the camera's right direction for horizontal movement
        Vector3 adjustedCamRight = cam.transform.right;
        adjustedCamRight.y = 0; // Ignore vertical tilt of the camera
        adjustedCamRight.Normalize();
        velocity += adjustedCamRight * hAxis * moveSpeed;

        // Get the camera's forward direction for vertical movement
        Vector3 adjustedCamForward = cam.transform.forward;
        adjustedCamForward.y = 0; // Ignore vertical tilt of the camera
        adjustedCamForward.Normalize();
        velocity += adjustedCamForward * vAxis * moveSpeed;

        // Gravity and Jumping
        if (cc.isGrounded)
        {
            // Reset yVelocity when on the ground to a small value so isGrounded is true 
            // for sure whenever we are near on the ground. (i.e. even after cc.Move prevented
            // us from going into the ground.
            yVelocity = -2; 

            // Check for jump input
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpForce; // Apply jump force
            }
        }
        else
        {
            // Apply gravity when in the air
            yVelocity += gravity * Time.deltaTime;
        }

        // Apply vertical velocity to movement
        velocity.y = yVelocity;

        // Limit movement speed
        velocity = Vector3.ClampMagnitude(velocity, 10);

        // Move the character
        cc.Move(velocity * Time.deltaTime);
    }
}
