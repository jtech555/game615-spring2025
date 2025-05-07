using UnityEngine;

public class PlayerDieOnCollision : MonoBehaviour
{
    public HeathManager playerHealth; // Reference to PlayerHealth script
    public GameObject iframe; // Reference to the iframe GameObject
    private bool isColliding = false; // To track if the player is still colliding
    private float timer = 0f; // Timer to track the 3-second wait
    private bool onClliderStil = false; // To ensure damage is only taken once during each interval

    private void OnTriggerEnter(Collider other)
    {
        // Check if it's the player colliding
        if (other.transform.CompareTag("Player") && isColliding == false)
        {
            onClliderStil = true; //on the spikes
            timer = 0f;
            playerHealth.TakeDamage(1); //take damage
        }
    }

    private void Update()
    {
        if (onClliderStil) //if on spikes
        {
            timer += Time.deltaTime; //start timer
            iframe.SetActive(Random.value > 0.5f); //iframe active 

            if (timer >= 3f) //if more than 3 seconds
            {
               
                playerHealth.TakeDamage(1); //take damage
                iframe.SetActive(false); //iframe off
                timer = 0f;
            }
        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        onClliderStil = false;
        iframe.SetActive(false);
    }
}
