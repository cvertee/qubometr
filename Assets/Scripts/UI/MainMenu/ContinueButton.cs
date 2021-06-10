using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        if (SaveSystem.GetAllSaves().Count <= 0)
        {
            Destroy(gameObject);
            return;
        }

        GetComponent<Button>().onClick.AddListener(() => ProfileSelectMenu.Show());
    }

    // Update is called once per frame
    private void Update()
    {
    }
}