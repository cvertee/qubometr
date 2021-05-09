using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [SerializeField] private float damage = 0.0f;
    [SerializeField] private List<string> attackNames;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<ITakesDamage>();
        damageable?.TakeDamage(damage); // TODO: replace?
    }

    private void Attack()
    {
        var randomAnimationIndex = Random.Range(0, attackNames.Count);
        var randomAnimationName = attackNames[randomAnimationIndex];
        animator.Play(randomAnimationName);
    }

    public override void Use()
    {
        Attack();
    }
}
