using System.Runtime.InteropServices;
using UnityEngine;

public class YouTubePlayer : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowYouTubePlayer(string videoId);

    [DllImport("__Internal")]
    private static extern void HideYouTubePlayer();

    public string youtubeVideoId = "7DmChQu5B9A"; // Your YouTube video ID

    public void PlayYouTubeVideo()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ShowYouTubePlayer(youtubeVideoId);
#endif
    }

    public void StopYouTubeVideo()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        HideYouTubePlayer();
#endif
    }
}
