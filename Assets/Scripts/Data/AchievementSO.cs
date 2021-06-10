using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "Achievements/Achievement", order = 0)]
public class AchievementSO : ScriptableObject
{
    public Sprite icon;
    public string displayName;
}
