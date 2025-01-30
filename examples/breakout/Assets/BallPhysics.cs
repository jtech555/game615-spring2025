using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    Vector3 velocity;
    Vector3 acceleration;

    [SerializeField]
    Camera cam;



    Vector3 gravity = new Vector3(0, -9.8f, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        acceleration = Vector3.zero;
        velocity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            acceleration += gravity;
        }

        Vector3 ballInScreenCoordinateSpace = cam.WorldToScreenPoint(transform.position);
        Vector3 directionToMouse = Input.mousePosition - ballInScreenCoordinateSpace;
        directionToMouse.z = 0;
        directionToMouse = directionToMouse.normalized;

        acceleration += directionToMouse * 9.8f;

        Debug.DrawRay(transform.position, acceleration/5, Color.green);
        
        velocity += acceleration * Time.deltaTime;

        velocity = Vector3.ClampMagnitude(velocity, 10);

        Debug.DrawRay(transform.position, velocity/5, Color.red);

        transform.position += velocity * Time.deltaTime;

        acceleration = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("paddle")) 
        {
            velocity.y *= -1;
        }
    }
}
