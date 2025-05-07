using UnityEngine;

public class facePlayerScript : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform

    void Update()
    {
        if (player != null)
        {
            // Rotate the game object to face the player
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0;  // Keep the rotation on the Y-axis (if you want to ignore the vertical rotation)

            // Rotate the object to face the player smoothly
            transform.rotation = Quaternion.LookRotation(directionToPlayer);
        }
    }
}
