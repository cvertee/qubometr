using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.UI.StoreComponents
{
    public class StoreBuyButton : MonoBehaviour
    {
        public UnityEvent onClick;

        // Start is called before the first frame update
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => onClick.Invoke());
        }

        public void Enable()
        {
            GetComponent<Button>().enabled = true;
        }

        public void Disable()
        {
            GetComponent<Button>().enabled = false;
        }
    }
}