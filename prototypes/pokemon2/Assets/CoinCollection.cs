using TMPro;
using UnityEngine;


public class CoinCollection : MonoBehaviour
{
    public TMP_Text Pokeballs;
    public int pokeballs = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "coin")
        {
            pokeballs++;
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
        Pokeballs.text = "Pokeballs: "+ pokeballs;
    }
}
