using System.Collections;
using UnityEngine;

public class Shield : Item
{
    private SpriteRenderer spriteRenderer;

    private float originalProtectionMultiplier;
    public float imperviousToDamageTime = 0.6f;

    private void Awake()
    {
        originalProtectionMultiplier = protectionMultiplier;

        spriteRenderer = GetComponent<SpriteRenderer>();

        StopUse();
    }

    public override void Use()
    {
        isBeingUsed = true;
        Show();
        StartCoroutine(nameof(BlockCoroutine));
    }

    private IEnumerator BlockCoroutine()
    {
        protectionMultiplier = 1.0f;
        Debug.Log($"Shield: full protection enabled");
        yield return new WaitForSeconds(imperviousToDamageTime);
        Debug.Log($"Shield: full protection disabled");
        protectionMultiplier = originalProtectionMultiplier;
    }

    public override void StopUse()
    {
        StopCoroutine(nameof(BlockCoroutine));
        protectionMultiplier = originalProtectionMultiplier;
        isBeingUsed = false;
        Hide();
    }

    private void Show()
    {
        spriteRenderer.enabled = true;
    }

    private void Hide()
    {
        spriteRenderer.enabled = false;
    }
}