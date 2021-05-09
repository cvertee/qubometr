using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinText : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    private void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        text.text = GameData.Data.coins.ToString() + " у.е.";
    }
}
