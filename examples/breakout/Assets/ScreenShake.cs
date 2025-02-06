using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    Vector3 startPosition;

    float screenShakeTimer = 0;

    void OnEnable() {
        BallPhysics.BallHitPaddleAction += BeginScreenShake;
    }

    void OnDisable() {
        BallPhysics.BallHitPaddleAction -= BeginScreenShake;
    }

    void BeginScreenShake() {
        Debug.Log("BALL HIT PADDLE! (I know this and I am the camera!)");
        StartCoroutine(ShakeScreen());
    }

    IEnumerator ShakeScreen() {
        float elapsedTime = 0;
        while(elapsedTime < 0.5f) {
            elapsedTime += Time.deltaTime;

            float offsetX = Random.Range(-1f, 1f);
            float offsetY = Random.Range(-1f, 1f);
            Vector3 shakeOffset = new Vector3(offsetX, offsetY, 0);
            transform.position += shakeOffset * 0.03f;

            yield return null;
        }

        transform.position = startPosition;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if (screenShakeTimer > 0) {
        //     screenShakeTimer -= Time.deltaTime;
        //     float offsetX = Random.Range(-1f, 1f);
        //     float offsetY = Random.Range(-1f, 1f);
        //     Vector3 shakeOffset = new Vector3(offsetX, offsetY, 0);
        //     transform.position += shakeOffset * 0.01f;
        // }
    }
}
