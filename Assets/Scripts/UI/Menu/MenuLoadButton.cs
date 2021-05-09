using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoadButton : MonoBehaviour
{
    private void OnEnable()
    {
        if (SaveSystem.GetSaveData() == null)
        {
            SaveSystem.Save();
        }
    }

    // Use this for initialization
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => { SaveSystem.Load(); });
    }
}