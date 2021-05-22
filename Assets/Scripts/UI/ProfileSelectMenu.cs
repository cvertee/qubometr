using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSelectMenu : MonoBehaviour
{
    private static ProfileSelectMenu Instance;

    public GameObject container;
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        
        container.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Show()
    {
        var allSaves = SaveSystem.GetAllSaves();
        if (allSaves.Count <= 0)
            return;

        Instance.container.SetActive(true);

        foreach (var save in allSaves)
        {
            var profileButtonObject = Resources.Load<Button>("Prefabs/UI/ProfileButton");
            var profileButton = Instantiate<Button>(profileButtonObject, Instance.container.transform);
            profileButton.GetComponentInChildren<Text>().text = save.profile;
            profileButton.onClick.AddListener(() =>
            {
                SaveSystem.SetCurrentProfile(save.profile);
                SaveSystem.Load();
            });
        }
    }
}
