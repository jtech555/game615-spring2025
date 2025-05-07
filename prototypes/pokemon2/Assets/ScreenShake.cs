using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 5f;
    public float shakeMagnitude = 5f;
    public float dampingSpeed = 1.0f;

    private Vector3 initialPosition;
    private float currentShakeTime = 0f;
    private bool isShaking = false;

    public AudioSource shakeAudioSource; // Assign in Inspector
    public bool startShake = false;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // TEMPORARY: Replace with your actual condition
        if (startShake == true)
        {
            StartCoroutine(ShakeScreen());
            startShake = false;
        }

        if (isShaking)
        {
            if (currentShakeTime > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
                currentShakeTime -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                currentShakeTime = 0f;
                isShaking = false;
                transform.localPosition = initialPosition;

                // Stop audio when shake ends
                if (shakeAudioSource != null && shakeAudioSource.isPlaying)
                    shakeAudioSource.Stop();
            }
        }
    }

    public IEnumerator ShakeScreen()
    {
        isShaking = true;
        currentShakeTime = shakeDuration;

        // Start audio if assigned
        if (shakeAudioSource != null)
            shakeAudioSource.Play();

        yield return new WaitForSeconds(shakeDuration);

        // Delay stopping handled in Update to sync with end of shake
    }
}
