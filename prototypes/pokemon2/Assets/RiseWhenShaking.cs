using UnityEngine;

public class RiseWhenShaking : MonoBehaviour
{
    public InkStoryManager inkstorymanager; // Assign this in the Inspector
    public float speed = 5f;

    void Update()
    {
        if (inkstorymanager != null && inkstorymanager.theShakeStart)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
    }
}
