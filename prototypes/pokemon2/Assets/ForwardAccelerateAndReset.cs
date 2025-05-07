using UnityEngine;

public class SteadyZMover : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 15f;
    public float travelDistance = 500f;

    private Vector3 startPosition;
    private float currentSpeed;
    private float distanceTraveled;

    void Start()
    {
        startPosition = transform.position;
        SetNewSpeed();
    }

    void Update()
    {
        float movement = currentSpeed * Time.deltaTime;
        transform.position += Vector3.forward * movement;
        distanceTraveled += movement;

        if (distanceTraveled >= travelDistance)
        {
            transform.position = startPosition;
            distanceTraveled = 0f;
            SetNewSpeed();
        }
    }

    void SetNewSpeed()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);
        Debug.Log("New Speed: " + currentSpeed);
    }
}
