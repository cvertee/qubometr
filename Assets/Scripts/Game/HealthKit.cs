using System.Collections;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameData.Data.hp >= 50) // TODO: fix
                return;

            GetComponent<SpriteRenderer>().enabled = false; // hide
            GetComponent<BoxCollider2D>().enabled = false; // to prevent several sounds playing
            AudioManager.Instance.PlaySound("estus");
            StartCoroutine(HealCoroutine());
        }
    }

    private IEnumerator HealCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        GameData.Data.hp += 10;
        GameData.Data.healthKitsUsed += 1;
        GameEvents.onHealthRestored.Invoke();
        Destroy(gameObject);
    }
}
