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
    private Vector3 moveDirection = Vector3.right;
    [SerializeField] private float sightDistance = 10.0f;
    private float sightDistanceFollowMultiplier = 1.5f;
    private CharacterState state = CharacterState.Idle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (hp <= 0)
            Die();
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

    private void OnIdle()
    {
        rb.velocity = Vector2.zero; // Stop move
        
        var playerHit = Physics2D.Raycast(
            transform.position, 
            Vector2.right, 
            sightDistance, 
            LayerMask.GetMask("Player")
        );
        if (playerHit.collider != null)
        {
            state = CharacterState.Follow;
            Debug.Log($"Player detected by {name}. Starting to follow him");
        }
    }
    private void OnFollow()
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
