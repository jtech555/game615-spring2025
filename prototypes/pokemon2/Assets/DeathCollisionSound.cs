using UnityEngine;

public class DeathCollisionSound : MonoBehaviour
{
    public GameObject itemDrop; 
    public Transform dropLocation; // Optional: location where the item will drop (can be the enemy's position)

    
    public void OnDestroy() //when enemy is dead
    {
        DropItem();
    }

    void DropItem()
    {
        // Check if itemDrop is assigned
        if (itemDrop != null)
        {
            Instantiate(itemDrop, dropLocation.position, Quaternion.identity);
        }
    }
}
