using UnityEngine;
using UnityEngine.Events;

public class AreaTrigger : DestroyerBase
{
    public UnityEvent onEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            onEnter.Invoke();
    } 

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
