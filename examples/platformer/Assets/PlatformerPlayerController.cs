using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject cam;
    CharacterController cc;
    Vector3 velocity = Vector3.zero;

    float yVelocity = 0;

    float moveSpeed = 12;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        velocity = Vector3.zero;

        Vector3 adjustedCamRight = cam.transform.right;
        adjustedCamRight.y = 0;
        adjustedCamRight.Normalize();
        velocity += adjustedCamRight * hAxis * moveSpeed;

        Vector3 adjustedCamForward = cam.transform.forward;
        adjustedCamForward.y = 0;
        adjustedCamForward.Normalize();
        velocity += adjustedCamForward * vAxis * moveSpeed;

        if (!cc.isGrounded) {
            yVelocity += -9.81f * Time.deltaTime;
        } else {
            yVelocity = -0.1f;

            if (Input.GetKeyDown(KeyCode.Space)) {
                yVelocity = 4;
            }
        }

        velocity.y += yVelocity;

        velocity = Vector3.ClampMagnitude(velocity, 10);
        cc.Move(velocity * Time.deltaTime);
    }
}
