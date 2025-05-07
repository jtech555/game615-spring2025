using UnityEngine;

public class OscillateX : MonoBehaviour
{
    public float moveDistance = 5f;   // Total distance to move
    public float moveSpeed = 0.1f;    // Speed per frame

    private Vector3 startPosition;
    private bool movingForward = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float step = moveSpeed;

        if (movingForward)
        {
            transform.position += new Vector3(step, 0f, 0f);

            if (transform.position.x >= startPosition.x + moveDistance)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.position -= new Vector3(step, 0f, 0f);

            if (transform.position.x <= startPosition.x)
            {
                movingForward = true;
            }
        }
    }
}
