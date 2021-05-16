using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventfulMonoBehaviour : MonoBehaviour
{
    public UnityEvent onStart;
    public UnityEvent onAwake;

    void Awake()
    {
        onAwake?.Invoke();
    }

    void Start()
    {
        onStart?.Invoke();
    }
}
