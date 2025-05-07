using UnityEngine;

public class CoinCollectPlaySound : MonoBehaviour
{
    public AudioSource audioSource;      

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")    
        {
            audioSource.Play();

        }
    }
}
