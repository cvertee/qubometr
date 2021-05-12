using UnityEngine.Events;

public class FloatEvent : UnityEvent<float> {}

public static class GameEvents
{
    public static UnityEvent onPlayerDeath = new UnityEvent();
    public static UnityEvent onEnemyAlert = new UnityEvent();
    public static UnityEvent onHealthRestored = new UnityEvent();
    public static UnityEvent onPlayerReceivedDamage = new UnityEvent();
    public static UnityEvent onCoinPickup = new UnityEvent();
    public static FloatEvent onCameraAreaEnter = new FloatEvent();
    public static UnityEvent onCameraAreaExit = new UnityEvent();
}