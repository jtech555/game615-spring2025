# Week 2

- Reading: Chapter 1 of Game Feel
    - "Good game feel is about making a game easy to learn but difficult to master. The enjoyment is in the learning, in the perfect balance between player skill and the challenge presented. Feelings of mastery bring their own intrinsic rewards."
- Polish: Animation system
- Prototype sharing
    - Feedback: 
        - Make a list of things "to look into"
- Knowledge sharing
    - Mike example: The Map function
    - Structure:    `
        - Show code, write code, use whiteboard
- Next week:
    - Two prototypes from groups of two
    - Gameplay involves something to get good at
    - Levels that demonstrate being bad, ok, and good at that
        - ~"the  motions experienced exist in the space of motions possible". Identify experiences you want your players to experience and design levels that require that.
            - Think about what types of constraints you've put between your player and the goals


```c#
public float Map(float valueOld, float oldMin, float oldMax, float newMin, float newMax)
{
    float oldRange = oldMax - oldMin;
    float newRange = newMax - newMin;
    float valueOldPercent = (valueOld - oldMin) / oldRange;
    return newRange * valueOldPercent + newMin;
}
```


```c#
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
        newVelocity.x += 10 * percent;
        newVelocity.y = Mathf.Max(newVelocity.y, newVelocity.y, 0.4f);
        newVelocity = newVelocity.normalized * previousVelocity.magnitude;
        rb.linearVelocity = newVelocity;
    }
}
```