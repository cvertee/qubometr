using System;
using Assets.Scripts.Game;
using UnityEngine;
using UnityEngine.Video;

namespace UI
{
    public class VideoWatch : MonoBehaviour
    {
        private void Start()
        {
            var videoClip = Resources.Load<VideoClip>($"Video/{GameData.Instance.videoToLoad}");
            var videoPlayer = FindObjectOfType<VideoPlayer>();
            videoPlayer.clip = videoClip;
            videoPlayer.Play();
        }
    }
}