using System;
using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public int hp = 10;
    public float moveSpeed = 10.0f;
    private float moveSpeedMultiplier = 1.0f;
    // TODO: decide if should use public or [...] protected 
    public float attackCooldownTime = 0.5f;
    public float attackCooldownTimeMultiplier = 1.0f;
    public float attackMinDistance = 3.0f;
    public float attackMaxDistance = 3.0f;
    public float attackMaxDistanceMultiplier = 1.0f;
    protected Vector3 moveDirection = Vector3.right;
    [SerializeField] private float sightDistance = 10.0f;
    private float sightDistanceFollowMultiplier = 1.5f;
    protected CharacterState state = CharacterState.Idle;
    protected bool canAttack = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (hp <= 0)
            Die();

        if (moveDirection == Vector3.left)
        {
            transform.eulerAngles = new Vector3(0f, -180f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f);
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case CharacterState.Idle:
                OnIdle();
                break;
            case CharacterState.Follow:
                OnFollow();
                break;
            case CharacterState.Attack:
                OnAttack();
                break;
            default:
                OnIdle();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>()
                .AddDamage();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.right);
    }

    protected virtual void OnIdle()
    {
        rb.velocity = Vector2.zero; // Stop move
        
        var playerHitRight = Physics2D.Raycast(
            transform.position, 
            Vector2.right, 
            sightDistance, 
            LayerMask.GetMask("Player")
        );
        var playerHitLeft = Physics2D.Raycast(
            transform.position, 
            Vector2.left, 
            sightDistance, 
            LayerMask.GetMask("Player")
        );
        if (playerHitRight.collider != null || playerHitLeft.collider != null)
        {
            state = CharacterState.Follow;
            Debug.Log($"Player detected by {name}. Starting to follow him");
        }
    }
    protected virtual void OnFollow()
    {
        var playerHitRight = Physics2D.Raycast(
            transform.position, 
            Vector2.right, 
            sightDistance * sightDistanceFollowMultiplier, 
            LayerMask.GetMask("Player")
        );
        if (playerHitRight.collider != null)
        {
            moveDirection = Vector3.right;
        }
        
        var playerHitLeft = Physics2D.Raycast(
            transform.position, 
            Vector2.left, 
            sightDistance * sightDistanceFollowMultiplier, 
            LayerMask.GetMask("Player")
        );
        if (playerHitLeft.collider != null)
        {
            moveDirection = Vector3.left;
        }
        var playerIsNear = Physics2D.Raycast(
            transform.position, 
            moveDirection, 
            attackMinDistance, 
            LayerMask.GetMask("Player")
        ).collider != null;

        if (playerIsNear)
        {
            state = CharacterState.Attack;
        }
        
        var hitWall = Physics2D.Raycast(
            transform.position, 
            moveDirection, 
            2.0f, // TODO: replace with const
            LayerMask.GetMask("Floor")
        );
        if (hitWall.collider != null)
        {
            state = CharacterState.Idle;
        }
        
        rb.velocity = new Vector2(
            moveDirection.x * moveSpeed * moveSpeedMultiplier * Time.fixedDeltaTime,
            rb.velocity.y
        );
    }

    protected virtual void OnAttack()
    {
        if (canAttack)
        {
            GetComponentInChildren<Animator>().Play("Attack");
            StartCoroutine(AttackCooldown());
        }
        
        var playerHit = Physics2D.Raycast(
            transform.position, 
            moveDirection, 
            attackMaxDistance * attackMaxDistanceMultiplier, 
            LayerMask.GetMask("Player")
        );
        if (playerHit.collider == null) // means enemy lost the player
        {
            state = CharacterState.Follow;
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldownTime * attackCooldownTimeMultiplier);
        canAttack = true;
    }

    public void AddDamage()
    {
        AudioManager.Instance.PlaySound("slashkut"); // OnDamage.Invoke() ?
        hp -= 10;
    }

    void Die()
    {
        Instantiate(Resources.Load("Prefabs/BloodParticle"), transform.position, Quaternion.identity);
        Instantiate(Resources.Load("Prefabs/Coin"), transform.position, Quaternion.identity);
        Destroy(gameObject);
        //GetComponent<BoxCollider2D>().enabled = false;
        //StartCoroutine(DieCoroutine());
    }
    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(1.25f);
        Destroy(gameObject);
    }
}
