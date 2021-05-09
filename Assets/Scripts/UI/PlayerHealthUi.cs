using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUi : MonoBehaviour
{
    private Slider slider;

    // Start is called before the first frame update
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    private void Update()
    {
        slider.value = GameData.Data.hp;
    }
}
