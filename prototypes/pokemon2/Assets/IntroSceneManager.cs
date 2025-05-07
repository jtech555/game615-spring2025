
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class IntroSceneManager : MonoBehaviour
{

    public AudioSource audioSource1;
    public AudioClip song2;

    public AudioClip song1;
    public AudioSource audioSource2;
    


    public GameObject f1;
    public GameObject f2;
    public GameObject f3;
    public GameObject f4;
    public GameObject f5;
    public GameObject f6;
    public GameObject f7;

    public int scene = 1;
    public bool stopMusic=false;
    private void Start()
    {
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource1.clip = song1;

        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource2.clip = song2;

        audioSource1.Play();
    }

    public void StopAudioSource2()
    {
        stopMusic = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scene++;
        }

        switch (scene)
        {
      
            case 1:
                {
                    f1.gameObject.SetActive(true);
                    f2.gameObject.SetActive(false);
                    f3.gameObject.SetActive(false);
                    f4.gameObject.SetActive(false);
                    f5.gameObject.SetActive(false);
                    f6.gameObject.SetActive(false);
                    f7.gameObject.SetActive(false);
                    break;
                }
            case 2:
                {
                    f1.gameObject.SetActive(false);
                    f2.gameObject.SetActive(true);
                    f3.gameObject.SetActive(false);
                    f4.gameObject.SetActive(false);
                    f5.gameObject.SetActive(false);
                    f6.gameObject.SetActive(false);
                    f7.gameObject.SetActive(false);
                    break;
                }
            case 3:
                {
                    f1.gameObject.SetActive(false);
                    f2.gameObject.SetActive(false);
                    f3.gameObject.SetActive(true);
                    f4.gameObject.SetActive(false);
                    f5.gameObject.SetActive(false);
                    f6.gameObject.SetActive(false);
                    f7.gameObject.SetActive(false);
                    break;
                }
            case 4:
                {
                    f1.gameObject.SetActive(false);
                    f2.gameObject.SetActive(false);
                    f3.gameObject.SetActive(false);
                    f4.gameObject.SetActive(true);
                    f5.gameObject.SetActive(false);
                    f6.gameObject.SetActive(false);
                    f7.gameObject.SetActive(false);
                    break;
                }
            case 5:
                {
                    f1.gameObject.SetActive(false);
                    f2.gameObject.SetActive(false);
                    f3.gameObject.SetActive(false);
                    f4.gameObject.SetActive(false);
                    f5.gameObject.SetActive(true);
                    f6.gameObject.SetActive(false);
                    f7.gameObject.SetActive(false);
                    break;
                }
            case 6:
                {
                    f1.gameObject.SetActive(false);
                    f2.gameObject.SetActive(false);
                    f3.gameObject.SetActive(false);
                    f4.gameObject.SetActive(false);
                    f5.gameObject.SetActive(false);
                    f6.gameObject.SetActive(true);
                    f7.gameObject.SetActive(false);
                    break;
                }
            case 7:
                {
                    f1.gameObject.SetActive(false);
                    f2.gameObject.SetActive(false);
                    f3.gameObject.SetActive(false);
                    f4.gameObject.SetActive(false);
                    f5.gameObject.SetActive(false);
                    f6.gameObject.SetActive(false);
                    f7.gameObject.SetActive(true);
                    break;
                }
            case 8:
                {
                    f1.gameObject.SetActive(false);
                    f2.gameObject.SetActive(false);
                    f3.gameObject.SetActive(false);
                    f4.gameObject.SetActive(false);
                    f5.gameObject.SetActive(false);
                    f6.gameObject.SetActive(false);
                    f7.gameObject.SetActive(false);
                    audioSource1.Stop();
                    
                    break;
                }
            
        }
        if (audioSource1.isPlaying == false)
        {
            if (audioSource2.isPlaying == false)
            {
                if(stopMusic!=true) audioSource2.Play();
            }
        }
    }
}