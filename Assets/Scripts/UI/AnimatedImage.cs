using UnityEngine;

public class AnimatedImage : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();

        GameEvents.onHealthRestored.AddListener(() => animator.Play("HealthRestored"));
        GameEvents.onPlayerReceivedDamage.AddListener(() => animator.Play("DamageReceived"));
        GameEvents.onCoinPickup.AddListener(() => animator.Play("CoinPickup"));
        GameEvents.onLocationStart.AddListener(() => animator.Play("BlackFadeOut"));
        GameEvents.onLocationChangeRequested.AddListener(() => animator.Play("BlackFadeIn"));
    }
}