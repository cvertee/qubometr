﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Stack<IPopupUIElement> popupUIElements = new Stack<IPopupUIElement>();

    private void Awake()
    {
        GameEvents.onCoinPickup.AddListener(ShowCoinPickupAnimation);
    }
    
    public void RegisterPopup(IPopupUIElement element)
    {
        popupUIElements.Push(element);
    }

    public bool CloseLatestPopup()
    {
        if (popupUIElements.Count <= 0)
            return false;

        var popupUIElement = popupUIElements.Pop();
        popupUIElement.Close();
        return true;
    }

    public void ShowCoinPickupAnimation()
    {
        var coinPickupText = Resources.Load("Prefabs/UI/CoinPickupText");
        Instantiate(coinPickupText, transform);
    }
}