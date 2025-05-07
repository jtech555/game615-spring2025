
using System.Collections.Generic;
using UnityEngine;
public class HeathManager : MonoBehaviour
{
    public GameObject Heath1;
    public GameObject Heath2;
    public GameObject Heath3;
    //public GameObject GameOver;

    public int health=3;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0) Debug.Log("died");

        switch (health)
        {
            case 0:
            {
                Heath1.gameObject.SetActive(false);
                Heath2.gameObject.SetActive(false);
                Heath3.gameObject.SetActive(false);
              //  GameOver.gameObject.SetActive(true);
                    break;
            }
            case 1:
            {
                Heath1.gameObject.SetActive(true);
                Heath2.gameObject.SetActive(false);
                Heath3.gameObject.SetActive(false);
              //  GameOver.gameObject.SetActive(false);
                    break;
            }
            case 2:
            {
                Heath1.gameObject.SetActive(true);
                Heath2.gameObject.SetActive(true);
                Heath3.gameObject.SetActive(false);
              //  GameOver.gameObject.SetActive(false);
                    break;
            }
            case 3:
            {
                Heath1.gameObject.SetActive(true);
                Heath2.gameObject.SetActive(true);
                Heath3.gameObject.SetActive(true);
             //   GameOver.gameObject.SetActive(false);
                    break;
            }
        }
    }
}