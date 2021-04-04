using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUi : MonoBehaviour
{
    private Slider slider;

    [SerializeField]
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = playerHealth.Value;
    }
}
