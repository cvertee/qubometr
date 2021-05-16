using System.Collections;
using UnityEngine;

public class HealthKit : PickableItemBase
{
    protected override void OnPickup()
    {

        GetComponent<SpriteRenderer>().enabled = false; // hide
        GetComponent<BoxCollider2D>().enabled = false; // to prevent several sounds playing
        AudioManager.Instance.PlaySound("estus");
        StartCoroutine(HealCoroutine());
    }

    private IEnumerator HealCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        GameData.Data.hp += 10;
        GameData.Data.healthKitsUsed += 1;
        GameEvents.onHealthRestored.Invoke();
        DestroySave();
    }
}
