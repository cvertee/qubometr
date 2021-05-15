using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupElementBase : MonoBehaviour, IPopupUIElement
{
    private void OnEnable() => UIManager.Instance.RegisterPopup(this);
    private void OnDisable() => Close();

    public void Close()
    {
        gameObject.SetActive(false);
        UIManager.Instance.CloseLatestPopup();
    }
}
