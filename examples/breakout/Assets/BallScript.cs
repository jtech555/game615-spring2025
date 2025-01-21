using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    float speed = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = new Vector3(0, -speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) 
    {   
        if (collision.gameObject.CompareTag("brick")) {
            Destroy(collision.gameObject);
        } else if (collision.gameObject.CompareTag("paddle")) {
            ContactPoint c = collision.GetContact(0);
            Vector3 paddlePos = collision.gameObject.transform.position;
            Vector3 paddleScale = collision.gameObject.transform.localScale;
            float percent = Map(c.point.x, paddlePos.x - paddleScale.x / 2, paddlePos.x + paddleScale.x / 2, -1, 1);
            Vector3 newVelocity = Vector3.up * collision.relativeVelocity.magnitude;
            rb.linearVelocity = newVelocity;
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
