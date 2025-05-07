using Unity.VisualScripting;
using UnityEngine;

public class AnimateSafe : MonoBehaviour
{
    public string animationStateName;

    private Animator animator;
    private bool hasPlayed = false;

    public PlayerController playerController;


    void Start()
    {
        GetComponent<Animator>().enabled = false;
    }


    private void Update()
    {
        if (playerController.leverActivate == true && hasPlayed == false)
        {
            GetComponent<Animator>().enabled = true;
            animator = GetComponent<Animator>();
            animator.Play(animationStateName, 0, 0f);
            hasPlayed = true;

        }
    }




}
