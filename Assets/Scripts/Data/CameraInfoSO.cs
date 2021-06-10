using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraInfo", menuName = "Camera/Camera info")]
public class CameraInfoSO : ScriptableObject
{
    public Vector3 offset = new Vector3(0, 0, -10);
}
