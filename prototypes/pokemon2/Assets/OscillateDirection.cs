using UnityEngine;

public class OscillateDirection : MonoBehaviour
{
    public float moveDistance = 5f;       // Total distance to move
    public float moveSpeed = 0.1f;        // Speed per frame
    public string direction = "up";       // Movement direction

    private Vector3 startPosition;
    private bool movingForward = true;
    private Vector3 movementAxis;

    void Start()
    {
        startPosition = transform.position;
        direction = direction.ToLower(); // Ensure lowercase for comparison

        // Set movement axis based on direction string
        switch (direction)
        {
            case "up":
                movementAxis = Vector3.up;
                break;
            case "down":
                movementAxis = Vector3.down;
                break;
            case "left":
                movementAxis = Vector3.left;
                break;
            case "right":
                movementAxis = Vector3.right;
                break;
            case "forward":
                movementAxis = Vector3.forward;
                break;
            case "backward":
                movementAxis = Vector3.back;
                break;
            default:
                Debug.LogWarning("Invalid direction set. Defaulting to up.");
                movementAxis = Vector3.up;
                break;
        }
    }

    void Update()
    {
        float step = moveSpeed;
        Vector3 currentOffset = transform.position - startPosition;
        float distanceMoved = Vector3.Dot(currentOffset, movementAxis.normalized);

        if (movingForward)
        {
            transform.position += movementAxis * step;

            if (distanceMoved >= moveDistance)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.position -= movementAxis * step;

            if (distanceMoved <= 0)
            {
                movingForward = true;
            }
        }
    }
}
