using UnityEngine;

public class PlayOhyeah : MonoBehaviour
{
    public AudioSource audioSource;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}