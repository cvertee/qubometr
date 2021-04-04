using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<VideoPlayer>().loopPointReached += IntroBehaviour_loopPointReached;
    }

    private void IntroBehaviour_loopPointReached(VideoPlayer source)
    {
        SceneManager.LoadScene("battlefield");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}