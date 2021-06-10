using System.Collections;
using UnityEngine;

public class HealthKit : PickableItemBase
{
    protected override void OnPickup()
    {
        if (GameData.GetHealth() >= 50) // TODO: fix
            return;

        GameEvents.onHealthKitUseStart.Invoke();

        GetComponent<SpriteRenderer>().enabled = false; // hide
        GetComponent<BoxCollider2D>().enabled = false; // to prevent several sounds playing
        StartCoroutine(HealCoroutine());
    }

    private IEnumerator HealCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        GameEvents.onHealthKitUseEnd.Invoke();
        DestroySave();
    }
}
