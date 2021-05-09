using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuExitButton : MonoBehaviour
{
    // Use this for initialization
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("mainmenu"));
    }

    // Update is called once per frame
    private void Update()
    {
    }
}