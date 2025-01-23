using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    public float minYReflection = 0.4f;

    public float initialSpeed = 7;

    public float distanceFromCenterInfluenceOnReflection = 10f;

    Vector3 previousVelocity;
    Vector3 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = new Vector3(0, -initialSpeed, 0);
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -8) {
            transform.position = startPosition;
            rb.linearVelocity = new Vector3(0, -initialSpeed, 0);
        }
    }

    void FixedUpdate()
    {
        previousVelocity = rb.linearVelocity;
    }

    void OnCollisionEnter(Collision collision) 
    {   
        if (collision.gameObject.CompareTag("brick")) {
            Destroy(collision.gameObject);
            rb.linearVelocity = new Vector3(previousVelocity.x, previousVelocity.y * -1, previousVelocity.z);
        } else if (collision.gameObject.CompareTag("paddle")) {
            ContactPoint c = collision.GetContact(0);
            Vector3 paddlePos = collision.gameObject.transform.position;
            Vector3 paddleScale = collision.gameObject.transform.localScale;
            float percent = Map(c.point.x, paddlePos.x - paddleScale.x / 2, paddlePos.x + paddleScale.x / 2, -1, 1);
            Vector3 newVelocity = new Vector3(previousVelocity.x, previousVelocity.y * -1, previousVelocity.z);
            newVelocity.x += distanceFromCenterInfluenceOnReflection * percent;
            newVelocity.y = Mathf.Max(newVelocity.y, minYReflection);
            newVelocity = newVelocity.normalized * previousVelocity.magnitude;
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
