# Week 4: 5 Minutes of Breakout Fun

## Grades

- Show [spreadsheet](https://docs.google.com/spreadsheets/d/1eIRH11AMV_cxXl6kAqPkP2pxomF30_MysSHzh-jVwhY/edit?usp=sharing) and explain color coding

## Play each other's games and consider the following:

- What were you thinking about as you played?
- What was the most exciting thing to happen?
    - Can we make it more exciting somehow?
- Do you feel like you became good at something?
- What made you frustrated?

## Professor Game Roast

## Adding "Juice" to your games

- Trail Renderer: Shows trajectory
        - Don't forget to create a gameobject and add a volume!
- Screen shake: Using perlin noise, coroutine for a certain amount of time, use aniationcurve.evalue to scale it over time.

```c#
    IEnumerator ShakeScreen() {
        float elapsed = 0;
        Vector3 originalPosition = transform.position;
        while(elapsed < 0.5f) {
            elapsed += Time.deltaTime;
            Vector3 offset = Vector3.zero;
            offset.x += Mathf.PerlinNoise(Time.time*10,0) * 2f - 1;
            offset.y += Mathf.PerlinNoise(0, Time.time*10) * 2f - 1;
            offset *= damp.Evaluate(elapsed/0.5f) * 0.2f;
            transform.position += offset;
            yield return null;
        }
        transform.position = originalPosition;
    }
```

- Slowing down time:

```c#
float slowMotionDistance = 3.0f; // Adjust as needed
float slowTimeScale = 0.1f;
float normalTimeScale = 1.0f;
float transitionSpeed = 8.0f; // Smoother transition
float distance = Vector3.Distance(transform.position, paddleObject.transform.position);
if (velocity.y < 0 && transform.position.y > paddleObject.transform.position.y && distance < slowMotionDistance)
{
    localTimeScale = Mathf.Lerp(localTimeScale, slowTimeScale, Time.unscaledDeltaTime * transitionSpeed);
} else {
    localTimeScale = Mathf.Lerp(localTimeScale, normalTimeScale, Time.unscaledDeltaTime * transitionSpeed);
}
```