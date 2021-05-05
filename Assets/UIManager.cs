using Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class UIManager : Singleton<UIManager>
    {
        private Stack<IPopupUIElement> popupUIElements = new Stack<IPopupUIElement>();

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
        
    }
}