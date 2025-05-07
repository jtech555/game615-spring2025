using TMPro;
using UnityEngine;

public class HealthAbovePlayersHead : MonoBehaviour
{
    public PlayerHealth playerHealth;
   
    [SerializeField] private TMP_Text textDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textDisplay.text=playerHealth.GetHealth().ToString();

    }
}
