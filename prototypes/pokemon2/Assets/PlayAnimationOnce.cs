using Unity.VisualScripting;
using UnityEngine;

public class PlayAnimationOnce : MonoBehaviour
{
    public string animationStateName;

    private Animator animator;
    private bool hasPlayed = false;

    public PlayerController playerController;


    void Start()
    {
        animator = GetComponent<Animator>();
        
    }


    private void Update()
    {
        if(playerController.leverActivate == true && hasPlayed==false)
        {
            animator.Play(animationStateName, 0, 0f);
            hasPlayed = true;

        }
    }




}
