using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Animator animator;
    
    [SerializeField] private float damage = 0.0f;
    [SerializeField] private List<string> attackNames;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<ITakesDamage>();
        damageable?.TakeDamage(damage); // TODO: replace?
    }

    public void Attack()
    {
        var randomAnimationIndex = Random.Range(0, attackNames.Count);
        var randomAnimationName = attackNames[randomAnimationIndex];
        animator.Play(randomAnimationName);
    }
}
