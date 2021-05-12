using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogues/Dialogue", order = 0)]
public class DialogueSO : ScriptableObject
{
    [TextArea(1, 1)]
    public string[] lines;
}
