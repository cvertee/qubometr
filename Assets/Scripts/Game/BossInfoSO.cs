using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boss", menuName = "Bosses/Boss", order = 0)]
public class BossInfoSO : ScriptableObject
{
    public string displayName;
    public float health;
    public AudioClip music;
}
