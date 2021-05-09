using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuExitButton : MonoBehaviour
{
    // Use this for initialization
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("mainmenu"));
    }

    // Update is called once per frame
    void Update()
    {
    }
}