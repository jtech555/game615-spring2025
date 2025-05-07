using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Linq;

public class DialogueManager : MonoBehaviour
{
    public bool dittoSnapped = false;
    public bool bulbasaurSnapped = false;
    public GameObject player;  // First game object
    public GameObject professor;  // Second game object
    public GameObject emma;  // Second game object
    public GameObject jasmine;  // Second game object

    public GameObject lever;  // Second game object

    public bool playLeverAnimation = false;

    public AudioSource jasmineCrying;


    private bool jamineCryingSoundActivated = false;


    public GameObject DialogueWindow;
    public float distanceThreshold = 5f;  // Distance within which the action happens
    public AudioSource helloThereAudio;
	bool helloThereActivated=false;
    public AudioSource thanksProfesorSound;
	public String line = "";
	bool thanksProfesorSoundActivated = false;
    public AudioSource wannaBattle;

    public bool turnMikeTextOff = false;
    public static SimpleConditionalConversation scc;

	public static Action<string, string> DialogueAction;

	// NOTE: When you do not use the google sheet option, it is expecting the file
	// to be named "data.csv" and for it to be in the Resources folder in Assets.
	public bool useGoogleSheet = false;
	public string googleSheetDocID = "";

    bool areYouOkActivated = false;
    public AudioSource areYouOk;

    public bool helicoptersAreUp = false;

    // Start is called before the first frame update
    void Start()
	{
		if (useGoogleSheet) {
			// This will start the asyncronous calls to Google Sheets, and eventually
			// it will give a value to scc, and also call LoadInitialHistory().
			GoogleSheetSimpleConditionalConversation gs_ssc = gameObject.AddComponent<GoogleSheetSimpleConditionalConversation>();
			gs_ssc.googleSheetDocID = googleSheetDocID;
		} else {
			scc = new SimpleConditionalConversation("data");
			LoadInitialSCCState();
		}
        

    }
	
	public static void LoadInitialSCCState()
	{
		// Example of setting the initial state:
		// NOTE: If you are putting a number or bool, make sure not to store them
		// as strings.
		//
		// scc.setGameStateValue("playerWearing", "equals", "Green shirt");
	}
	
	// Update is called once per frame
	void Update()
	{


        if (helicoptersAreUp == true)
        {
            scc.setGameStateValue("hunger", "equals", 29);
           
            if (turnMikeTextOff == false)
            {
                DialogueWindow.SetActive(true);
                line = DialogueManager.scc.getSCCLine("Mike");
                GameManager.instance.DisplayText("Mike", line);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                wannaBattle.Play();
                turnMikeTextOff = true;
                
            }
            if (turnMikeTextOff == true )
            {
                line = DialogueManager.scc.getSCCLine("Ash");
                GameManager.instance.DisplayText("Ash", line);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                DialogueWindow.SetActive(false);
                
            }
        }
        else
        {


            // Check if the objects are within the specified distance from each other
            float distance = Vector3.Distance(player.transform.position, professor.transform.position);
            float distanceFromEmma = Vector3.Distance(player.transform.position, emma.transform.position);
            float distanceFromJasmine = Vector3.Distance(player.transform.position, jasmine.transform.position);

            float distanceFromLever = Vector3.Distance(player.transform.position, lever.transform.position);


            if (distanceFromLever <= distanceThreshold && Input.GetKeyDown(KeyCode.E))
            {
                playLeverAnimation = true;
                Debug.Log("lever hit");

            }

                if (distance <= distanceThreshold && Input.GetKeyDown(KeyCode.E))
            {
                if (bulbasaurSnapped == true && dittoSnapped == true)
                {
                    scc.setGameStateValue("picture", "equals", 3);
                }
                else
                {
                    if (dittoSnapped == true) scc.setGameStateValue("picture", "equals", 2);
                    else if (bulbasaurSnapped == true) scc.setGameStateValue("picture", "equals", 1);
                }

                DialogueWindow.SetActive(true);
                line = DialogueManager.scc.getSCCLine("Mike");

                GameManager.instance.DisplayText("Mike", line);
                if (helloThereActivated == false)
                {
                    helloThereActivated = true;
                    helloThereAudio.Play();
                }
            }


            if (distanceFromJasmine < 50)
            {
                // if (line == "Great Scotts! You did it! Time to photoshop these pictures and e-mail them to the Viridian Mayor! Good work Ash!")
                // {
                  if (jamineCryingSoundActivated == false)
                 {
                    jamineCryingSoundActivated = true;
             jasmineCrying.Play();

                }
                // }
                //  DialogueWindow.SetActive(false);
            }
            else jasmineCrying.Stop();






            if (distance > distanceThreshold && distanceFromEmma > distanceThreshold)
            {
                if (line == "Great Scotts! You did it! Time to photoshop these pictures and e-mail them to the Viridian Mayor! Good work Ash!")
                {
                    if (thanksProfesorSoundActivated == false)
                    {
                        thanksProfesorSoundActivated = true;
                        thanksProfesorSound.Play();

                    }
                }
                DialogueWindow.SetActive(false);
            }




            if (distanceFromEmma <= distanceThreshold && Input.GetKeyDown(KeyCode.E))
            {

                DialogueWindow.SetActive(true);
                line = DialogueManager.scc.getSCCLine("Emma");

                GameManager.instance.DisplayText("Emma", line);
                if (areYouOkActivated == false)
                {
                    areYouOkActivated = true;
                    areYouOk.Play();

                }
            }


        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            scc.setGameStateValue("questState", "equals", 1);
        }

            // if (Input.GetKeyDown(KeyCode.Space)) {


            //	Debug.Log("Mike says: " + line);
            //	DialogueAction?.Invoke("Mike", line);
            //}

            // An example of modifying the state outside of the DialogueManager (e.g. you could put this in some
            // OnTriggerEnter or something)

	}
}
