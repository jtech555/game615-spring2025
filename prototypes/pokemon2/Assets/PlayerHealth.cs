using JetBrains.Annotations;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public int health;
    public int maxHealth = 10;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("got hit");
        health -= amount;
        if (health <= 0)
        { 
            Destroy(gameObject);
        }
    }

    public int GetHealth()
    {
        return health;
    }


    void Update()
    {
        
    }
}
