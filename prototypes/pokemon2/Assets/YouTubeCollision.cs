using UnityEngine;
using System.Runtime.InteropServices;

public class YouTubeProximityTrigger : MonoBehaviour
{
    public GameObject gameObject1; // The target (e.g., the player)
    public float triggerDistance = 30f;
    public string youtubeEmbedURL = "https://www.youtube.com/embed/7DmChQu5B9A?start=15&autoplay=1&fs=1";

    private bool hasLaunched = false;

    public PlayerController playerController;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void ShowYouTubePlayer(string url);
#endif

    void Update()
    {
        if (hasLaunched || gameObject1 == null) return;

        float distance = Vector3.Distance(transform.position, gameObject1.transform.position);

        if (distance <= triggerDistance)
        {
            playerController.gameHasEnded = true;

#if UNITY_WEBGL && !UNITY_EDITOR
            ShowYouTubePlayer(youtubeEmbedURL);
#else
            Application.OpenURL(youtubeEmbedURL);
#endif

            hasLaunched = true;
        }
    }
}
