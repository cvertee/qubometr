using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickupText : MonoBehaviour
{
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
