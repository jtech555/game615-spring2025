using UnityEngine;

public class MoveOnLever : MonoBehaviour
{
    public PlayerController playerController;

    private Vector3 targetPosition;
    private bool shouldMove = false;

    private float moveSpeed = 0.5f;
    private float moveDistance = 0.5f;
    private bool targetSet = false;

    void Update()
    {
        if (playerController.leverActivate && !targetSet)
        {
            targetPosition = transform.position - new Vector3(0f, 0f, moveDistance);
            targetSet = true;
            shouldMove = true;
            Debug.Log("statement working");
        }

        if (shouldMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            Debug.Log("statement2 working");
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                shouldMove = false; // stop moving once reached
            }
        }
    }
}
