using System.Collections;
using UnityEngine;

public class Shield : Item
{
    private SpriteRenderer spriteRenderer;

    private float originalProtectionMultiplier;
    public float imperviousToDamageTime = 0.6f;

    private void Awake()
    {
        originalProtectionMultiplier = data.protectionMultiplier;

        spriteRenderer = GetComponent<SpriteRenderer>();

        StopUse();
    }

    public override void Use()
    {
        data.isBeingUsed = true;
        Show();
        StartCoroutine(nameof(BlockCoroutine));
    }

    private IEnumerator BlockCoroutine()
    {
        data.protectionMultiplier = 1.0f;
        Debug.Log($"Shield: full protection enabled");
        yield return new WaitForSeconds(imperviousToDamageTime);
        Debug.Log($"Shield: full protection disabled");
        data.protectionMultiplier = originalProtectionMultiplier;
    }

    public override void StopUse()
    {
        StopCoroutine(nameof(BlockCoroutine));
        data.protectionMultiplier = originalProtectionMultiplier;
        data.isBeingUsed = false;
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