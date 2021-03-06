using System;
using UnityEngine;
using UnityEngine.Events;

public class FloatEvent : UnityEvent<float> {}
public class GameSettingEvent : UnityEvent<string> {}
public class AudioClipEvent : UnityEvent<AudioClip> {}
public class AudioStringEvent : UnityEvent<string> {}
public class DelayedActionEvent : UnityEvent<float, Action> {}

public static class GameEvents
{
    public static UnityEvent onLocationStart = new UnityEvent();
    public static UnityEvent onPlayerDeath = new UnityEvent();
    public static UnityEvent onEnemyAlert = new UnityEvent();
    public static FloatEvent onPlayerHealthModified = new FloatEvent();
    public static UnityEvent onHealthKitUseStart = new UnityEvent();
    public static UnityEvent onHealthKitUseEnd = new UnityEvent();
    public static UnityEvent onHealthRestored = new UnityEvent();
    public static UnityEvent onPlayerReceivedDamage = new UnityEvent();
    public static UnityEvent onCoinPickup = new UnityEvent();
    public static FloatEvent onCameraAreaEnter = new FloatEvent();
    public static UnityEvent onCameraAreaExit = new UnityEvent();
    public static UnityEvent onLocationChangeRequested = new UnityEvent();
    public static GameSettingEvent onGameSettingChanged = new GameSettingEvent();
    public static UnityEvent onPopupUiElementShowed = new UnityEvent();
    public static UnityEvent onPopupUiElementsEnded = new UnityEvent();
    public static AudioClipEvent onAudioClipPlayRequested = new AudioClipEvent();
    public static AudioStringEvent onAudioNamePlayRequested = new AudioStringEvent();
    public static DelayedActionEvent onDelayedActionRequested = new DelayedActionEvent();
}