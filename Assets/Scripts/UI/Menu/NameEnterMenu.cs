using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameEnterMenu : MonoBehaviour
{
    private static NameEnterMenu Instance;
    
    public GameObject container;
    public InputField inputField;
    public Button acceptButton;

    private void Start()
    {
        Instance = this;
        
        container.SetActive(false);
        
        acceptButton.onClick.AddListener(() =>
        {
            var profileName = inputField.text;

            if (string.IsNullOrEmpty(profileName) || string.IsNullOrWhiteSpace(profileName))
                return;
            
            SaveSystem.SetCurrentProfile(profileName);
            SaveSystem.Save();
            Hide();
        });
    }

    public static void Show()
    {
        Instance.container.SetActive(true);
    }

    public static void Hide()
    {
        Instance.container.SetActive(false);
    }
}
