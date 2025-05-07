using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    public EnemyHealth enemyHealth;
    public int damage = 1;
    private void Awake()
    {
        Destroy(gameObject,life);
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }

    }

}
