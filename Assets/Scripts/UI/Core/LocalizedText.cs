using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public string id;

    void OnEnable()
    {
        GetComponent<Text>().text = LocalizationUtil.IdToLocalized(id);
    }

}
