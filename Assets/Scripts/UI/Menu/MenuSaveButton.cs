using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuSaveButton : MonoBehaviour
{
    // Use this for initialization
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (string.IsNullOrEmpty(SaveSystem.GetCurrentProfile()))
            {
                NameEnterMenu.Show();
                return;
            }
            
            SaveSystem.Save();
            UIManager.Instance.CloseLatestPopup(); // should be menu
        });
    }
}