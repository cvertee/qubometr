using System.Collections;
using UnityEngine;

public class MenuUI : Singleton<MenuUI>, IPopupUIElement
{
    public GameObject menuContainer;

    void Awake()
    {
        Close();
    }

    public void Show()
    {
        UIManager.Instance.RegisterPopup(this);
        menuContainer.SetActive(true);
    }

    public void Close()
    {
        menuContainer.SetActive(false);
    }
}