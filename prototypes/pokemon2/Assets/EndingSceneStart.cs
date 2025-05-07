
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class EndingSceneStart : MonoBehaviour
{

    public AudioSource audioSource1;
    public AudioClip song1;

    public GameObject f1;
    public GameObject f2;
    public GameObject f3;
    public GameObject f4;
    public GameObject f5;
    public GameObject f6;
    public GameObject f7;
    public GameObject f8;
    public GameObject f9;

    public bool start = false;
    public int scene = 1;
    public IntroSceneManager introSceneManager;

    private void Start()
    {
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource1.clip = song1;

        f1.gameObject.SetActive(false);
        f2.gameObject.SetActive(false);
        f3.gameObject.SetActive(false);
        f4.gameObject.SetActive(false);
        f5.gameObject.SetActive(false);
        f6.gameObject.SetActive(false);
        f7.gameObject.SetActive(false);
        f8.gameObject.SetActive(false);
        f9.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            start = true;
            audioSource1.Play();
        }
    }

        // Update is called once per frame
        void Update()
    {
        if(start==true)
        {
            introSceneManager.StopAudioSource2();

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
                        f8.gameObject.SetActive(false);
                        f9.gameObject.SetActive(false);
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
                        f8.gameObject.SetActive(false);
                        f9.gameObject.SetActive(false);
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
                        f8.gameObject.SetActive(false);
                        f9.gameObject.SetActive(false);
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
                        f8.gameObject.SetActive(false);
                        f9.gameObject.SetActive(false);
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
                        f8.gameObject.SetActive(false);
                        f9.gameObject.SetActive(false);
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
                        f8.gameObject.SetActive(false);
                        f9.gameObject.SetActive(false);
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
                        f8.gameObject.SetActive(false);
                        f9.gameObject.SetActive(false);
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
                        f8.gameObject.SetActive(true);
                        f9.gameObject.SetActive(false);
                        break;
                    }
                case 9:
                    {
                        f1.gameObject.SetActive(false);
                        f2.gameObject.SetActive(false);
                        f3.gameObject.SetActive(false);
                        f4.gameObject.SetActive(false);
                        f5.gameObject.SetActive(false);
                        f6.gameObject.SetActive(false);
                        f7.gameObject.SetActive(false);
                        f8.gameObject.SetActive(false);
                        f9.gameObject.SetActive(true);
                        break;
                    }
            }
            
        }
     
    }
}