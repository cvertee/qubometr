using System;
using UnityEngine;
using UnityEngine.Video;

public class VideoWatch : MonoBehaviour
{
    private void Start()
    {
        var videoClip = Resources.Load<VideoClip>($"Video/{GameData.Data.videoToLoad}");
        var videoPlayer = FindObjectOfType<VideoPlayer>();
        videoPlayer.clip = videoClip;
        videoPlayer.Play();
    }
}