using UnityEngine;
using System.Collections;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;
using System.Collections.Specialized;
using System;
using UnityEngine.Audio;
//using GLTFast.Schema;
//using GLTFast.Schema;

public class InkStoryManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset inkJSONAsset = null;

    [SerializeField]
    private GameObject choicePrefab;

    [SerializeField]
    private GameObject choicesUI;

    [SerializeField]
    private GameObject textBoxUI;

    [SerializeField]
    private TMP_Text textBox;

  //  public AudioSource notification;
  //  public AudioSource promotionSound;

   // public TMP_Text Rank;


    // We are going to use this to make the coroutine wait until a choice button 
    // is clicked.
    private bool choiceMade = false;

    // We can use this to know whether a knot is currently running. I use it to
    // control whether I can launch TalkToCharacter or not.
    public bool knotActive = false;

    Story inkStory;

    public Camera targetCamera;


    public GameObject gameObject1; //player
    public GameObject gameObject2; //mad hatter
    public GameObject gameObject3; //profesor
    public GameObject gameObject4; //bridgeman
    public GameObject gameObject5; //mew
    public GameObject pikachu; //pikachu
    public GameObject mewsPikachu;

    public GameObject startScreen; //startscreen
    public GameObject gameOverScreen;

    public GameObject GroundInFrontOfProfessor;

    public GameObject fire; //mad hatter
    public GameObject sphere;
    public GameObject bridgeBoundary;
    public GameObject panel;
    public bool explosionOver=false;
    private bool hatManKnotStarted = false;
    private bool professorKnotStarted = false;
    private bool manByTheBridgeKnotStarted = false;
    private bool mewKnotStarted = false;


    //private bool stormTrooperKnotStarted = false;
    //private bool stormTrooperKnot2Started = false;
    //private bool femaleLegoKnotStarted = false;
    //private bool femaleLegoKnotStarted2 = false;

    // public GameObject image; // Assign the 2D image GameObject in the Inspector

    //public GameObject wolf;

    // private bool soundActivate = false;
    public bool explosionSeen = false;
    public AudioSource crying;
    public AudioSource areYouOk;
    public AudioSource explosion;
    public AudioSource teleportSound;
    public AudioSource haventSeenAnyone;
    public string line = "";

    public bool introSoundPlayed = false;

    public float growthSpeed = 5f;
    public float maxScale = 20f;
    public float fadeSpeed = 2f;

    private bool isFading = false;
    private Renderer sphereRenderer;
    private Color originalColor;

    public GameObject mewTwoInHospital;
    private Vector3 mewTwoStartPosition;
    private bool mewTwoisMoving = true;
    public float mewTwospeed = 5f;
    private float mewTwodistanceToMove = 24f;

    public bool theShakeStart = false;

    public CoinCollection coinCollection;

    public CameraShake cameraShake;


    private Animator vaultAnimator;
    // Name of the animation state you want to play
    public string animationStateName = "doorAnimator";


    void Start()
    {
        vaultAnimator = GetComponent<Animator>();

        //     cameraShake.TriggerShake();
        mewTwoStartPosition = mewTwoInHospital.transform.position;
        //  image.SetActive(true); // Show the image at start

        inkStory = new Story(inkJSONAsset.text);

        StartCoroutine(LaunchKnot("IntroductoryScene"));
        inkStory.variablesState["person1"] = "The Mad Hatter";
        inkStory.variablesState["player"] = "Mike";
        inkStory.variablesState["status"] = 0;
        inkStory.variablesState["pokeballs"] = 0;
        inkStory.variablesState["choice"] = 0;
        inkStory.variablesState["talk1Over"] = "false";
        inkStory.variablesState["talk2Over"] = "false";
        inkStory.variablesState["bridgeUnlocked"] = "false";
        inkStory.variablesState["shakeStart"] = "false";
        inkStory.variablesState["mewConvoEnded"] = "false";
        //inkStory.variablesState["pokeballsRemoved"] = "false";
        // textBoxUI.setActive(true);



        if (inkStory.variablesState["mewConvoEnded"].ToString() == "true") Debug.Log("step over");



        if (sphere != null)
        {
            sphereRenderer = sphere.GetComponent<Renderer>();
            if (sphereRenderer != null)
            {
                originalColor = sphereRenderer.material.color;
            }
            else
            {
                Debug.LogWarning("Renderer not found on sphere.");
            }
        }
    }



    bool IsInView(Camera cam, GameObject obj)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        Bounds bounds = obj.GetComponent<Renderer>().bounds;
        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }




    IEnumerator LaunchKnot(string knotName)
    {
        knotActive = true;
        // Set ink to use the knotName that was provided
        inkStory.ChoosePathString(knotName);

        textBoxUI.SetActive(true);

        while (inkStory.canContinue)
        {
            // As long as there are no choices, keep displaying lines in
            // the text box, and waiting for the player to press space.
            while (inkStory.canContinue)
            {
                line = inkStory.Continue().Trim();
                Debug.Log(line);
                // Display the line in the text box
                textBox.text = line;

                // Wait for input
                while (!Input.GetKeyDown(KeyCode.Space))
                {
                    yield return null; // Wait for next frame
                }
                yield return null; // This is necessary because the loop continued when space was pressed and registered it as being pressed again.
            }

            // If there are any choices, wait for the choice to be made. We make this
            // a while because there may be two sets of choice in a row.
            while (inkStory.currentChoices.Count > 0)
            {
                choicesUI.SetActive(true);

                // Display all the choices, if there are any!
                for (int i = 0; i < inkStory.currentChoices.Count; i++)
                {
                    Choice choice = inkStory.currentChoices[i];

                    // Create a button object from the prefab
                    GameObject buttonObj = Instantiate(choicePrefab, choicesUI.transform);

                    // Get the Button and TMP_Text components
                    Button button = buttonObj.GetComponent<Button>();
                    TMP_Text choiceText = buttonObj.GetComponentInChildren<TMP_Text>();

                    // Safety check: log error if setup is wrong
                    if (button == null)
                    {
                        Debug.LogError("Choice prefab is missing a Button component!");
                        continue;
                    }
                    if (choiceText == null)
                    {
                        Debug.LogError("Choice prefab is missing a TMP_Text component!");
                        continue;
                    }

                    // Set the choice text
                    choiceText.text = choice.text;
                    Debug.Log("Creating button for choice: " + choice.text);

                    // ? FIX: Capture the choice index correctly
                    int choiceIndex = choice.index;

                    // Add the onClick listener to choose the option
                    button.onClick.AddListener(() =>
                    {
                     //   notification.Play();
                        Debug.Log("Choice selected: " + choice.text);
                        inkStory.ChooseChoiceIndex(choiceIndex);
                        choiceMade = true;
                        RemoveChoiceButtons();
                    });
                }

                // Wait for the button function above to be called, which sets
                // choiceMade to true, so we will move on in the coroutine
                while (!choiceMade)
                {
                    yield return null;
                }

                choiceMade = false; // reset this
                choicesUI.SetActive(false);
            }
        }

        // Turn off the UI now that the knot is over
        textBoxUI.SetActive(false);
        knotActive = false;
        Debug.Log("KNOT COMPLETE!");
    }

    void RemoveChoiceButtons()
    {
        int childCount = choicesUI.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Destroy(choicesUI.transform.GetChild(i).gameObject);
        }
    }

    public void TalkToCharacter()
    {
        // You need to reset the story if you want it to happen over and over
        // In this way, it might make sense to have a different ink file for 
        // each interaction.
        inkStory = new Story(inkJSONAsset.text);
        StartCoroutine(LaunchKnot("TalkToCharacter"));
    }





    void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (vaultAnimator != null)
            {
                vaultAnimator.Play(animationStateName);
                Debug.Log("Playing animation: " + animationStateName);
            }
        }




        if (inkStory.variablesState["shakeStart"].ToString() == "true")
        {
            theShakeStart = true;
            
            pikachu.SetActive(false);
            mewsPikachu.SetActive(true);
        }







        if (gameObject1.transform.position.y < -80f)
        {
           // gameOverScreen.SetActive(true);
        }

        if (inkStory.variablesState["talk2Over"].ToString() == "true")
        {
            if (mewTwoisMoving && mewTwoInHospital != null)
            {
                mewTwoInHospital.transform.Translate(Vector3.up * mewTwospeed * Time.deltaTime);

                if (Vector3.Distance(mewTwoStartPosition, mewTwoInHospital.transform.position) >= mewTwodistanceToMove)
                {
                    mewTwoisMoving = false;
                    // Snap exactly to 10 units just in case we overshoot a little.
                    mewTwoInHospital.transform.position = mewTwoStartPosition + Vector3.up * mewTwodistanceToMove;
                }
            }
        }

        
        if (inkStory.variablesState["shakeStart"].ToString() == "true")
        {
            GroundInFrontOfProfessor.SetActive(false);
           // cameraShake.startShake = true;
        }

        if (inkStory.variablesState["bridgeUnlocked"].ToString() == "true")
        {
            bridgeBoundary.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            startScreen.SetActive(false); // Hide the image when space is pressed
            if (introSoundPlayed == false)
            {
                haventSeenAnyone.Play();
                introSoundPlayed = true;
            }
        }

        


        inkStory.variablesState["pokeballs"] = coinCollection.pokeballs;
        // Rank.text = "Knight's Watch Rank: "+inkStory.variablesState["status"].ToString();
        // Pokeballs.text = "Pokeballs: "+inkStory.variablesState["pokeballs"].ToString();

        if (line == "He took them all! All my Pokemon!")
        {
            areYouOk.Play();
            line = "He took them all! All my Pokemon! ";
        }


        if (inkStory.variablesState["talk2Over"].ToString() == "true")
        {
            teleportSound.Play();
        }

        if (explosionSeen == false && inkStory.variablesState["talk1Over"].ToString()=="true")
        {
            if (IsInView(targetCamera, sphere))
            {
                explosion.Play();
                explosionSeen = true;
            }
        }

        if (explosionSeen == true)
        {
            fire.SetActive(true);
            if (!isFading)
            {
                // Increase the sphere's scale
                sphere.transform.localScale += Vector3.one * growthSpeed * Time.deltaTime;

                if (sphere.transform.localScale.x >= maxScale)
                {
                    isFading = true;
                }
            }
            else
            {
                // Fade out the sphere
                if (sphereRenderer != null)
                {
                    Color newColor = sphereRenderer.material.color;
                    newColor.a -= fadeSpeed * Time.deltaTime;
                    sphereRenderer.material.color = newColor;

                    // Destroy the sphere once fully transparent
                    if (newColor.a <= 0f)
                    {
                        Destroy(sphere);
                        explosionOver = true;
                    }
                }
            }
        }




        if (Vector3.Distance(gameObject1.transform.position, gameObject2.transform.position) <= 70f)
        {
            if (crying.isPlaying == false)
            {
                crying.Play();
            }
        }
        else if ((Vector3.Distance(gameObject1.transform.position, gameObject2.transform.position) > 70f))
        {
            crying.Stop();
        }
       
        

        

        if (hatManKnotStarted == false && Vector3.Distance(gameObject1.transform.position, gameObject2.transform.position) <= 5f)
        {
            StartCoroutine(LaunchKnot("hatManKnot"));
            hatManKnotStarted = true;
            //panel.SetActive(true);
        }



        if (professorKnotStarted == false && Vector3.Distance(gameObject1.transform.position, gameObject3.transform.position) <= 5f)
        {
            StartCoroutine(LaunchKnot("Professor"));
            professorKnotStarted = true;
          //  panel.SetActive(false);
        }

      

        if (manByTheBridgeKnotStarted == false && Vector3.Distance(gameObject1.transform.position, gameObject4.transform.position) <= 5f)
        {
            //  inkStory.ResetState();


            inkStory.ResetState();
            
            panel.SetActive(true);
            inkStory.variablesState["pokeballs"] = coinCollection.pokeballs;
            if (explosionSeen == true) inkStory.variablesState["talk1Over"] = true;
            else inkStory.variablesState["talk1Over"] = false;

            inkStory.ChoosePathString("ManByTheBridge");
            StartCoroutine(LaunchKnot("ManByTheBridge"));

            manByTheBridgeKnotStarted = true;
        }

        if (Vector3.Distance(gameObject1.transform.position, gameObject4.transform.position) > 20f)
        {
           // panel.SetActive(false);
            manByTheBridgeKnotStarted = false;
        }


        if (mewKnotStarted == false && Vector3.Distance(gameObject1.transform.position, gameObject5.transform.position) <= 15f)
        {
            StartCoroutine(LaunchKnot("Mew"));
            mewKnotStarted = true;
            panel.SetActive(true);
        }

        if (Vector3.Distance(gameObject1.transform.position, gameObject5.transform.position) > 20f)
        {
             panel.SetActive(false);
           
        }


        //  if (manByTheBridgeKnotStarted == false && Vector3.Distance(gameObject1.transform.position, gameObject4.transform.position) > 5f)
        //  {
        //      manByTheBridgeKnotStarted = false;
        //  }
        Debug.Log(inkStory.variablesState["talk1Over"].ToString());
    }




}
