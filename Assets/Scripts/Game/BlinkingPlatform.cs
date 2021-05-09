using System.Collections;
using UnityEngine;

public class BlinkingPlatform : MonoBehaviour
{
    [SerializeField] private float aliveTime;
    [SerializeField] private float blinkTime;
    [SerializeField] private float timeOffset;
    private float colliderDisableDelay = 0.25f;

    private void Start()
    {
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        yield return new WaitForSeconds(timeOffset);

        while (true)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSecondsRealtime(colliderDisableDelay);
            GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(blinkTime);

            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            GetComponent<BoxCollider2D>().enabled = true;
            yield return new WaitForSeconds(aliveTime);
        }
    }
}