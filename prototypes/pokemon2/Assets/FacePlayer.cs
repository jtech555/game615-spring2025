using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform player; // Drag your player object into this field in the Inspector

    void Update()
    {
        if (player != null)
        {
            // Make the object face the player
            transform.LookAt(player);
        }
    }
}
