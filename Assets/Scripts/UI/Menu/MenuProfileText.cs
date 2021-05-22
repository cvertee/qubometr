using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuProfileText : MonoBehaviour
{
    private void OnEnable() => GetComponent<Text>().text = $"Профиль: {SaveSystem.GetCurrentProfile()}" ?? "";
}
