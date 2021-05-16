using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        FindObjectOfType<VideoPlayer>().loopPointReached += IntroBehaviour_loopPointReached;
    }

    private void IntroBehaviour_loopPointReached(VideoPlayer source)
    {
        SceneManager.LoadScene("warStart");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<VideoPlayer>().Stop();
            IntroBehaviour_loopPointReached(null);
        }
    }
}
