using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Stack<IPopupUIElement> popupUIElements = new Stack<IPopupUIElement>();

    private void Awake()
    {
        GameEvents.onCoinPickup.AddListener(ShowCoinPickupAnimation);
    }

    public bool HasPopupElements => popupUIElements.Count > 0;
    
    public void RegisterPopup(IPopupUIElement element)
    {
        popupUIElements.Push(element);
        GameEvents.onPopupUiElementShowed.Invoke();
    }

    public bool CloseLatestPopup()
    {
        // If there aren't any popup ui elements
        if (popupUIElements.Count <= 0)
        {
            GameEvents.onPopupUiElementsEnded.Invoke();
            return false;
        }

        var popupUIElement = popupUIElements.Pop();
        popupUIElement.Close();
        
        // means last popup ui element was used
        if (popupUIElements.Count <= 0)
        {
            GameEvents.onPopupUiElementsEnded.Invoke();
        }
        return true;
    }

    public void ShowCoinPickupAnimation()
    {
        var coinPickupText = Resources.Load("Prefabs/UI/CoinPickupText");
        Instantiate(coinPickupText, transform);
    }
}