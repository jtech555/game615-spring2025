using System.Collections;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public TextMeshProUGUI textDisplay; 

    void Start()
    {
        if (textDisplay != null)
        {
            StartCoroutine(RoutineDelayedText("My Love...", 0.5f)); 
        }
        else
        {
            Debug.LogError("TextMeshProUGUI reference is missing. Assign it in the Inspector.");
        }
    }

    private IEnumerator RoutineDelayedText(string textToDisplay, float timeDelay)
    {
        textDisplay.text = ""; 

        for (int i = 0; i < textToDisplay.Length; ++i)
        {
            textDisplay.text = textToDisplay.Substring(0, i + 1); 
            yield return new WaitForSeconds(timeDelay); 
        }
    }
}
