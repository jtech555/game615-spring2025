using JetBrains.Annotations;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int health;
    public int maxHealth = 10;

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
       
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


    // Update is called once per frame
    void Update()
    {

    }
}
