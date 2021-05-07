﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
    public class MenuExitButton : MonoBehaviour
    {

        // Use this for initialization
        void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene("mainmenu"));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}