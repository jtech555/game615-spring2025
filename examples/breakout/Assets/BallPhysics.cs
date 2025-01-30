using UnityEngine;

// This component assumes that the game object using it has a RigidBody with all Constraints checked
// i.e. x, y, z frozen for position and rotation.
[RequireComponent(typeof(Rigidbody))]
public class BallPhysics : MonoBehaviour
{
    // Store the start position in Start. We'll use this to reset the start position when the ball
    // passes by the ball.
    Vector3 startPosition;

    // The rate of change of velocity. In physics, objects move when forces are applied to them.
    // These forces only affect the movement of objects when they are continually applied. Because
    // of this, we will accumulate all forces acting on this object at the start of the Update
    // function, and then we will apply those forces to the velocity. It is important that we
    // 'zero out' the acceleration vector, allowing the Update function to accumulate all forces.
    // This set up allows us to selectively add foces to this object (e.g. I only apply the gravity 
    // force if the spacebar is pressed).
    Vector3 acceleration;
    // The rate of change of position
    Vector3 velocity;
    Vector3 previousVelocity; // We'll use this to get the movement vector in OnCollisionEnter

    // The value for this is assigned in the Unity editor (drag the main camera into the spot in the inspector)
    [SerializeField]
    Camera cam;

    // This is the value of gravity (what happens if we change this?)
    Vector3 gravity = new Vector3(0, -9.8f, 0);


    // These controls affect the way that the ball reflects off of the paddle
    public float minYReflection = 0.4f;
    public float initialSpeed = 7;
    public float distanceFromCenterInfluenceOnReflection = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;

        acceleration = Vector3.zero; // Same as "new Vector3(0, 0, 0);"
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // NOTE: See the above extended comment above the Vector3 acceleration definition for a
        // description of how this works.

        // Clear acceleration, and then add all forces the the acceleration.
        acceleration = Vector3.zero;

        // ---- APPLY FORCES ----
        
        // Apply gravity
        acceleration += gravity;

        // If the mouse button is held, compute a force vector to where the mouse is on the screen
        // and apply that force to the ball.
        if (Input.GetMouseButton(0)) {
            // Figure out where the object would be in the screen coordinates using the built in function
            // This is a function that is part of the camera, that is why we need to get a reference to 
            // the camera above
            Vector3 ballInScreenCoordinateSpace = cam.WorldToScreenPoint(transform.position);
            Vector3 directionToMouse = Input.mousePosition - ballInScreenCoordinateSpace;
            // Zero out the z values
            directionToMouse.z = 0;

            // Normalize the vector we just computed to set it to a standard length that isn't dependent
            // on the distance from the mouse to the ball, and then scale that vector by how strong we 
            // want this "attracting" force to be. In this example, I just used 15 because that was stronger
            // than gravity, so I could play around with preventing the ball from falling.
            directionToMouse = directionToMouse.normalized;

            // Add this force to the accleration
            acceleration += directionToMouse * 15f;
        }

        // This is an example of how we can visualize information about the movement of the object.
        Debug.DrawRay(transform.position, acceleration/5, Color.green);
        
        // Add the acceleration (scaled by Time.deltaTime) to the velocity.
        velocity += acceleration * Time.deltaTime;

        // Prevent the velocity from getting too fast. This will maintain the direction, but cap the
        // magnitude of the vector at 10.
        velocity = Vector3.ClampMagnitude(velocity, 10);

        // Draw another debug ray to visualize this velocity vector.
        Debug.DrawRay(transform.position, velocity/5, Color.red);

        // Finally, actually move the object by adding the velocity vector (scaled by Time.deltaTime)
        // to the position.
        transform.position += velocity * Time.deltaTime;

        // Check to see if we went past the paddle
        if (transform.position.y < -5) {
            // Reset to the start position, and start velocity
            transform.position = startPosition;
            velocity = Vector3.zero;
        }

        previousVelocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint cp = collision.GetContact(0);
        if (collision.gameObject.CompareTag("paddle")) 
        {
            // This is my personal (not necessarily the best or most straight forward) implementation
            // of having control over the ball's reflection when it collides with the ball.
            Vector3 paddlePos = collision.gameObject.transform.position;
            Vector3 paddleScale = collision.gameObject.transform.localScale;
            float percent = Map(cp.point.x, paddlePos.x - paddleScale.x / 2, paddlePos.x + paddleScale.x / 2, -1, 1);
            Vector3 newVelocity = new Vector3(previousVelocity.x, previousVelocity.y * -1, previousVelocity.z);
            newVelocity.x += distanceFromCenterInfluenceOnReflection * percent;
            newVelocity.y = Mathf.Max(newVelocity.y, minYReflection);
            newVelocity = newVelocity.normalized * previousVelocity.magnitude;
            velocity = newVelocity;
        } else {
            // Do a normal reflection behavior
            velocity = Vector3.Reflect(velocity, cp.normal);

            // We can use the collision "normal" (the vector pointing away from thr surface), to add
            // a little bit to our position to ensure that we are not still colliding with the 
            // surface. This help[s prevent the "sticking" that can happen.
            transform.position += cp.normal * 0.01f;
        }
    }

    public float Map(float valueOld, float oldMin, float oldMax, float newMin, float newMax)
    {
        float oldRange = oldMax - oldMin;
        float newRange = newMax - newMin;
        float valueOldPercent = (valueOld - oldMin) / oldRange;
        return newRange * valueOldPercent + newMin;
    }
}