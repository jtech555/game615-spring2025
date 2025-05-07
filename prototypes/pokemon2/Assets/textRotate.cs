using System.Collections;
using TMPro;
using UnityEngine;

public class TextRotate : MonoBehaviour
{


    [SerializeField] private TMP_Text textDisplay;

    
    public string[] messages = new string[10] 
    {
        "Message 1", "Message 2", "Message 3", "Message 4", "Message 5",
        "Message 6", "Message 7", "Message 8", "Message 9", "Message 10"
    };

    private int currentIndex = 0; 

    void Start()
    {
        if (textDisplay != null)
        {
            StartCoroutine(DisplayMessages());
        }
        else
        {
            Debug.LogError("TextMeshProUGUI reference is missing! Assign it in the Inspector.");
        }
    }

    private IEnumerator DisplayMessages()
    {
        while (true) 
        {
            textDisplay.text = messages[currentIndex]; 
            currentIndex = (currentIndex + 1) % messages.Length; 

            yield return new WaitForSeconds(9f); 
        }
    }
}
