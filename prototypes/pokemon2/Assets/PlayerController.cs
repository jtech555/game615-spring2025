//using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public DialogueManager DialogueManager;

    public Transform childObject;
    public Transform cameraTransform;






    public GameObject lever;
    public bool leverActivate = false;





    public float cameraDistance = 25f; // Default camera distance
    public float cameraHeight = 2f;   // How high the camera is from the ground level

    float rotateSpeed = 110f;
    float forwardSpeed = 38.5f;
    float jumpForce = 20f;
    float gravity = -19.8f;

    float yVelocity = 0;
    public GameObject AshThrow;
    public GameObject AshWalk;
    public GameObject AshJump;
    public GameObject AshIdle;
    public GameObject AshClimb;
    public GameObject AshFall;
    public GameObject Pika;
    public GameObject bulbasaur;  // Second game object
    public GameObject ditto;  // Second game object

    public InkStoryManager inkStoryManager;

    public Camera theCamera;


    public GameObject mewRun;

    public CharacterController cc;
    public Animator animator;

    public Animator pikaAnimator;

    private int jumpCount = 0;
    private const int maxJumps = 2;

    public float climbSpeed = 3f;
    private bool isGoingUpLadder = false;

    public float stompSpeed = -80f;
    private bool isStomping = false;

  

    private bool isMKeyHeld = false;  // To track if "M" key is held down
    public AudioSource audioSource;
    public AudioSource walkingSound;
    public AudioSource startSound;
    public AudioSource pikaHappySound;
    //public AudioSource squirtleSound;
    public AudioSource thunderbolt;
    public AudioSource chooseYou;
    public AudioSource teleport;
    public AudioSource whatIsThat;



    public GameObject pickachuVideoPlayer;
    public GameObject lightning;
    public GameObject snorlax;
    private VideoPlayer videoPlayer;
    int snorlaxMove = 0;
    int counter = 0;

    public AudioSource bgMusic;
    public AudioSource darkMusic;
    public AudioSource acdcMusic;
    //public AudioSource ballOpenSound;
    bool cameraOnViridian = false;

    bool choppersGoUp=false;
    public GameObject choppers;
    public GameObject chopperLightening;

    public int throwCounter = 0;

    public bool pointersOnFix = false;
    public bool pointersOn=false;

    private float timer = 0f; // The time elapsed since the key was pressed

    public bool timerStart = false;

    public GameObject squirtle; // The GameObject you want to position

    private float mewforwardSpeed = 53f;
    private int shiftCycleCount = 0;

    public bool coroutineStarted = false;


    public bool gameHasEnded = false;







    void Start()
    {
        
        // Get the AudioSource component attached to this GameObject
        // audioSource = GetComponent<AudioSource>();

        // Update the camera position at the start
        UpdateCameraPosition();
       // startSound.Play();

        videoPlayer = pickachuVideoPlayer.GetComponent<VideoPlayer>();

        bgMusic.Play();
        bgMusic.loop = true;

       
    }

    void PlayVideo()
    {
        // Ensure the GameObject is active and play the video
        pickachuVideoPlayer.SetActive(true);
        videoPlayer.Play();

        // Add an event listener to detect when the video finishes
        videoPlayer.loopPointReached += VideoEnded;
    }

    void VideoEnded(VideoPlayer vp)
    {
        // Deactivate the GameObject when the video ends
        pickachuVideoPlayer.SetActive(false);
    }



    private System.Collections.IEnumerator ShiftRoutine()
    {
        yield return new WaitForSeconds(4f);

        while (shiftCycleCount < 3)
        {
            if (shiftCycleCount == 1) whatIsThat.Play();
            yield return new WaitForSeconds(2f);

            // Shift left by 6 on X axis
            mewRun.transform.position = new Vector3(mewRun.transform.position.x - 6f, mewRun.transform.position.y, mewRun.transform.position.z);
            teleport.Play();
            yield return new WaitForSeconds(2f);

            // Shift right by 6 on X axis
            mewRun.transform.position = new Vector3(mewRun.transform.position.x + 6f, mewRun.transform.position.y, mewRun.transform.position.z);
            teleport.Play();
            shiftCycleCount++;
        }
    }




    void Update()
    {
        if (gameHasEnded == true) darkMusic.Stop();
        if (inkStoryManager.theShakeStart == true)
        {
            bgMusic.Stop();
            if(gameHasEnded==true) darkMusic.Stop();
            else if(darkMusic.isPlaying==false) darkMusic.Play();
          
        }


        if (inkStoryManager.introSoundPlayed == true && coroutineStarted==false)
        {
            StartCoroutine(ShiftRoutine());
            coroutineStarted = true;
        }

        if(coroutineStarted==true) mewRun.transform.Translate(Vector3.forward * mewforwardSpeed * Time.deltaTime);






        if (pointersOnFix == false)
        {
            if (pointersOn == true)
            {
                // Get all children of the copter
                foreach (Transform child in choppers.transform)
                {
                    // Create a line renderer for each child (if you don't want to create it dynamically, add it to each child in the scene)
                    LineRenderer lineRenderer = child.GetComponent<LineRenderer>();

                    if (lineRenderer == null)
                    {
                        // If the child does not have a LineRenderer, create one
                        lineRenderer = child.gameObject.AddComponent<LineRenderer>();
                    }

                    // Set the number of points for the line (start and end)
                    lineRenderer.positionCount = 2;

                    // Set the start and end points of the line
                    lineRenderer.SetPosition(0, child.position);  // Start point (child position)

                    // Add a random y offset to the target's position
                    Vector3 targetPos = AshIdle.transform.position;
                    float randomYOffset = Random.Range(-5f, 6.5f); // Random y offset within a range (adjust this range as needed)

                    // Temporarily apply the random y offset to the target's position
                    Vector3 targetWithYOffset = new Vector3(targetPos.x, targetPos.y + randomYOffset, targetPos.z);

                    lineRenderer.SetPosition(1, targetWithYOffset);  // End point (target position)

                    // Set the line color to red
                    lineRenderer.startColor = new Color(0.2f, 0f, 0f);  // Darker Red
                    lineRenderer.endColor = new Color(0.2f, 0f, 0f);    // Darker Red

                    // Set the width of the line
                    lineRenderer.startWidth = 0.05f;
                    lineRenderer.endWidth = 0.05f;

                    // Optionally, set other properties like material if needed (can be done in the inspector)
                }
            }
        }
        else
        {
            // When pointersOnFix is true, disable the line renderers
            foreach (Transform child in choppers.transform)
            {
                LineRenderer lineRenderer = child.GetComponent<LineRenderer>();
                if (lineRenderer != null)
                {
                    lineRenderer.enabled = false;  // Disable the line renderer
                }
            }
        }









        if (inkStoryManager.explosionSeen == true && inkStoryManager.explosionOver == false)
        {
            AshFall.SetActive(true);
            AshIdle.SetActive(false);
            AshThrow.SetActive(false);
            AshWalk.SetActive(false);
        }
        else
        {
            AshFall.SetActive(false);

            if (Input.GetKeyDown(KeyCode.T))
            {
                AshThrow.SetActive(true);
                AshIdle.SetActive(false);
                timer = 0f;
                timerStart = true;
                chooseYou.Play();
                squirtle.SetActive(false);
            }

            if (timerStart == true)
            {
                timer += Time.deltaTime;
                if (timer >= .001)// ballOpenSound.Play(); 
                    if (timer >= 3.55f)
                    {
                        AshThrow.SetActive(false);


                        if (timer >= 4.55f)
                        {
                            //squirtleSound.Play();
                            timerStart = false;
                        }


                    }
                if (timer >= 2.5)
                {
                    // Position the object 10 units in front of the target object
                    Vector3 positionInFront = AshIdle.transform.position + squirtle.transform.forward * 10f;
                    squirtle.SetActive(true);
                    // Set the new position
                    squirtle.transform.position = positionInFront;
                }
            }

            //if (Input.GetKeyUp(KeyCode.T)// & isGoingUpLadder == false && AshJump.activeSelf == false)                               //
            //{





        


        if (Input.GetKeyDown(KeyCode.P))
        {
            thunderbolt.Play();
            PlayVideo();

            if (choppersGoUp == true)
            {
                snorlaxMove = 2;
            }
            else snorlaxMove = 1;
        }

        if (snorlaxMove == 2)
        {
            counter++;
            if (counter < 1900)
            {
                pointersOn = false;
                chopperLightening.SetActive(true);
                if (counter > 1000)
                {
                    pikaHappySound.Play();
                    pointersOnFix = true;
                    chopperLightening.SetActive(false);
                    choppers.transform.position = new Vector3(choppers.transform.position.x + Random.Range(-2f, -5f), choppers.transform.position.y, choppers.transform.position.z);
                }
            }
            else
            {

            }
        }

        if (snorlaxMove == 1)
        {
            counter++;
            if (counter < 1900)
            {
                if (counter > 900)
                {
                    lightning.SetActive(true);
                    snorlax.transform.position = new Vector3(-53.35f, snorlax.transform.position.y, snorlax.transform.position.z);
                    snorlax.transform.position = new Vector3(snorlax.transform.position.x + Random.Range(-10f, 10f), snorlax.transform.position.y, snorlax.transform.position.z);
                }
            }
            else
            {
                snorlax.transform.position = new Vector3(-53.35f, snorlax.transform.position.y, snorlax.transform.position.z);
                counter = 0;
                snorlaxMove = 0;
                lightning.SetActive(false);
            }
        }
           

            if (Input.GetKeyDown(KeyCode.W) && AshJump.activeSelf == false)
        {

                
                pikaAnimator.SetTrigger("walk");

            AshWalk.SetActive(true);
            AshIdle.SetActive(false);
            walkingSound.Play();
        }
        if (Input.GetKeyUp(KeyCode.W) & isGoingUpLadder == false && AshJump.activeSelf == false)                               //
        {
            walkingSound.Stop();
            pikaAnimator.SetTrigger("stop");
            AshWalk.SetActive(false);
            AshIdle.SetActive(true);
      
        }



    }



            if (Input.GetKeyDown(KeyCode.M)) // When M is pressed down
            {
                isMKeyHeld = true;
                cameraDistance = 5f;  // Set camera distance to 5 when M is held down

                //ash pika
                //  Ash.SetActive(false);
                Pika.SetActive(false);
                AshWalk.SetActive(false);
                AshIdle.SetActive(false);
                AshJump.SetActive(false);

            }
            if (Input.GetKeyUp(KeyCode.M) == true)
            {
                isMKeyHeld = false;
                cameraDistance = 25f;  // Set camera distance back to 25 when M is released
                Pika.SetActive(true);
                AshIdle.SetActive(true);

                if (DialogueManager.line == "The e-mail has been sent. Maybe see if you can get on the roof and snap a pic of their reactions!")
                {

                    bgMusic.Stop();
                    bgMusic.loop = false;
                    acdcMusic.Play();

                    choppersGoUp = true;
                }
            }

            if (choppersGoUp == true)
            {
                float distanceToMove = choppers.transform.position.y - 0;
                if (distanceToMove < 30f)
                {
                    // Move the object up at the specified speed
                    choppers.transform.Translate(Vector3.up * 5f * Time.deltaTime);


                    pointersOn = true;



                }
                if (distanceToMove >= 20f)
                {
                    DialogueManager.helicoptersAreUp = true;
                }
            }                                                                                           //copter

            if (isMKeyHeld && Input.GetKeyDown(KeyCode.K))
            {
                PlayAudio();



                if (bulbasaur.GetComponent<Renderer>().isVisible)
                {
                    DialogueManager.bulbasaurSnapped = true;
                }

                if (ditto.GetComponent<Renderer>().isVisible)
                {
                    DialogueManager.dittoSnapped = true;
                }
            }


        float distanceFromLever = Vector3.Distance(AshIdle.transform.position, lever.transform.position);


        if (distanceFromLever <= 10f && Input.GetKeyDown(KeyCode.E))
        {
            leverActivate = true;
            Debug.Log("lever hit");

        }


        // Get horizontal (left/right) and vertical (forward/back) axis inputs
        float hAxis = Input.GetAxis("Horizontal");
            float vAxis = Input.GetAxis("Vertical");

            // Handle ladder climbing
            if (isGoingUpLadder)
            {
                Pika.transform.localPosition = new Vector3(.22f, 5.34f, -.94f);

                AshJump.SetActive(false);
                AshClimb.SetActive(true);
                AshWalk.SetActive(false);
                AshIdle.SetActive(false);

                Vector3 climbMovement = new Vector3(0, vAxis * climbSpeed, 0);
                cc.Move(climbMovement * Time.deltaTime);

                if (Input.GetKey(KeyCode.Q))
                {
                    Vector3 leftMovement = -transform.right * forwardSpeed * Time.deltaTime;
                    cc.Move(leftMovement);
                }
                if (Input.GetKey(KeyCode.E))
                {
                    Vector3 rightMovement = transform.right * forwardSpeed * Time.deltaTime;
                    cc.Move(rightMovement);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    isGoingUpLadder = false;
                    yVelocity = jumpForce;
                    Vector3 jumpDirection = -transform.forward + transform.up;
                    cc.Move(jumpDirection * jumpForce * Time.deltaTime);

                }

            }
            else
            {
                Pika.transform.localPosition = new Vector3(2.52f, -1f, 0f);
                AshClimb.SetActive(false);


            if (inkStoryManager.explosionOver == true)
            {


                if (!AshIdle.activeSelf && !AshWalk.activeSelf && !AshJump.activeSelf && !isMKeyHeld && !AshThrow.activeSelf)
                {
                    walkingSound.Stop();
                    AshIdle.SetActive(true);
                }
                // Jumping logic when grounded
                if (cc.isGrounded)
                {
                    jumpCount = 0;
                    yVelocity = -5;
                    isStomping = false;
                    if (Input.GetKeyDown(KeyCode.Space))  // Jump only on pressing Space
                    {
                        yVelocity = jumpForce;
                        jumpCount++;
                        pikaAnimator.SetTrigger("jump");
                        AshJump.SetActive(true);
                        AshClimb.SetActive(false);
                        AshWalk.SetActive(false);
                        AshIdle.SetActive(false);
                    }
                    else
                    {
                        AshJump.SetActive(false);
                        pikaAnimator.SetTrigger("stop");

                    }
                    if (Input.GetKeyUp(KeyCode.Space))  // Jump only on pressing Space
                    {
                        AshJump.SetActive(false);
                        pikaAnimator.SetTrigger("stop");

                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
                    {
                        yVelocity = jumpForce;
                        jumpCount++;
                        childObject.localPosition = Vector3.zero;
                        childObject.localRotation = Quaternion.identity;

                    }

                    if (jumpCount == maxJumps && Input.GetKeyDown(KeyCode.S))
                    {
                        isStomping = true;
                    }

                    if (isStomping)
                    {
                        yVelocity = stompSpeed;
                    }
                    else
                    {
                        if (yVelocity < 0)
                        {

                            yVelocity += gravity / 2 * Time.deltaTime;
                        }
                        else
                        {

                            yVelocity += gravity * Time.deltaTime;

                        }
                    }

                    if (Input.GetKeyUp(KeyCode.Space) && yVelocity > 0)
                    {

                        //AshClimb.SetActive(false);
                        //AshWalk.SetActive(false);
                        //AshIdle.SetActive(false);
                        yVelocity = 0;
                    }

                }


            }

                // Handle movement and rotation
                // Only horizontal axis should affect rotation (left/right turning)
                if (hAxis != 0)
                {
                    float rotationAngle = hAxis * rotateSpeed * Time.deltaTime;
                    transform.Rotate(0, rotationAngle, 0);
                }

                // Forward/backward movement
                Vector3 forwardMovement = transform.forward * forwardSpeed * vAxis;
                forwardMovement.y = yVelocity;  // Set the vertical component (jumping/falling)
                cc.Move(forwardMovement * Time.deltaTime);

            
        }

        // Update camera position to always stay behind the player
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        // Position the camera behind the player based on the player's rotation
        Vector3 desiredPosition = transform.position - transform.forward * cameraDistance + Vector3.up * cameraHeight;
        cameraTransform.position = desiredPosition;

        // Ensure the camera is always looking at the back of the player
        cameraTransform.LookAt(transform.position + Vector3.up * cameraHeight);
    }

    private void PlayAudio()
    {
        // Check if the audio clip is set and is not already playing
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();  // Play the audio
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Ladder"))
        {
            isGoingUpLadder = true;
            yVelocity = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isGoingUpLadder = false;
        }
    }
}
