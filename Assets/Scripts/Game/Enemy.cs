using System;
using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Core;
using Game;
using System.Text;

public class Enemy : MonoBehaviour, ITakesDamage, ICharacter
{
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    
    public float hp = 10;
    private float currentMoveSpeed = 0.0f;
    public float moveSpeed = 10.0f;
    private float moveSpeedMultiplier = 1.0f;
    // TODO: decide if should use public or [...] protected 
    public float attackCooldownTime = 0.5f;
    public float attackCooldownTimeMultiplier = 1.0f;
    public float attackMinDistance = 3.0f;
    public float attackMaxDistance = 3.0f;
    public float attackMaxDistanceMultiplier = 1.0f;
    public float collisionDamageCooldownTime = 1.0f; // Time in which box collider of enemy is disabled
    protected Vector3 moveDirection = Vector3.right;
    private float aiTickTime = 0.1f;
    [SerializeField] private float sightDistance = 10.0f;
    private float sightDistanceFollow;
    private float sightDistanceForWall = 2.0f;
    private float sightDistanceForPlayerTooNear = 0.4f;
    //private float sightDistanceFollowMultiplier = 1.5f;
    protected CharacterState state = CharacterState.Idle;
    protected bool canAttack = true;
    private Item usableItem;

    private void Awake()
    {
        sightDistanceFollow = sightDistance * 2.0f;

        if (GameData.Data.killedEnemies.Any(x => x == name))
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();

        if ((usableItem = GetComponentInChildren<Weapon>()) == null) // means enemy doesn't have any weapons
        {
            GameManager.Instance.AddItemById("Knife", this);
        }
    }

    private void Start()
    {
        StartCoroutine(AIUpdate());
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
        rb.velocity = new Vector2(
            moveDirection.x * currentMoveSpeed * moveSpeedMultiplier * Time.deltaTime,
            rb.velocity.y
        );
    }

    private IEnumerator AIUpdate()
    {
        while (true)
        {
            AIStateCheck();
            yield return new WaitForSecondsRealtime(aiTickTime);
        }
    }
    private void AIStateCheck()
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
        if (!collider.enabled)
            return;
        
        // TODO: false until `double collision` (sword AND enemy) is fixed or replaced with something else
        if (false)
        {
            collision.GetComponent<ITakesDamage>()
                .TakeDamage(10.0f); // TODO: use variable

            //StartCoroutine(CollisionDamageCooldown());
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawRay(transform.position, Vector3.right);
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(sightDistance * moveDirection.x, 0));
    }

    private void OnGUI()
    {
        var pos = UnityEngine.Camera.main.WorldToScreenPoint(transform.position);
        var sb = new StringBuilder();

        sb.AppendLine($"state={state}");

        GUI.Label(new Rect(pos.x, pos.y, 400, 400), sb.ToString());
    }

    // Stands and waits for player at two sides
    protected virtual void OnIdle()
    {
        currentMoveSpeed = 0.0f;
        //rb.velocity = Vector2.zero; // Stop move
        
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
            GameEvents.onEnemyAlert.Invoke();
            Debug.Log($"Player detected by {name}. Starting to follow him");
        }
    }
    protected virtual void OnFollow()
    {
        currentMoveSpeed = moveSpeed;

        var playerRightCast = Physics2D.Raycast(
            transform.position, 
            Vector2.right,
            sightDistanceFollow, 
            LayerMask.GetMask("Player")
        );
        var playerLeftCast = Physics2D.Raycast(
            transform.position, 
            Vector2.left,
            sightDistanceFollow, 
            LayerMask.GetMask("Player")
        );
        var playerNearCast = Physics2D.Raycast(
            transform.position, 
            moveDirection, 
            attackMinDistance, 
            LayerMask.GetMask("Player")
        );
        var wallNearCast = Physics2D.Raycast(
            transform.position,
            moveDirection,
            sightDistanceForWall,
            LayerMask.GetMask("Floor")
        );

        if (playerRightCast.collider != null)
        {
            moveDirection = Vector3.right;
        }
        if (playerLeftCast.collider != null)
        {
            moveDirection = Vector3.left;
        }
        if (playerNearCast.collider != null)
        {
            state = CharacterState.Attack;
        } 
        if (wallNearCast.collider != null)
        {
            state = CharacterState.Idle;
        }
    }

    protected virtual void OnAttack()
    {
        if (canAttack && usableItem != null)
        {
            currentMoveSpeed = 0f; // Stop before the attack
            usableItem.Use();
            StartCoroutine(AttackCooldown());
        }
        
        var playerHit = Physics2D.Raycast(
            transform.position, 
            moveDirection, 
            attackMaxDistance * attackMaxDistanceMultiplier, 
            LayerMask.GetMask("Player")
        );
        var playerTooNearLeftCast = Physics2D.Raycast(
            transform.position,
            Vector2.left,
            sightDistanceForPlayerTooNear,
            LayerMask.GetMask("Player")
        );
        var playerTooNearRightCast = Physics2D.Raycast(
            transform.position,
            Vector2.left,
            sightDistanceForPlayerTooNear,
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

    public void TakeDamage(float damage)
    {
        AudioManager.Instance.PlaySound("slashkut"); // OnDamage.Invoke() ?
        Instantiate(Resources.Load("Prefabs/BloodParticle"), transform.position, Quaternion.identity);
        hp -= damage; //TODO: use damage var
    }

    void Die()
    {
        Debug.Log($"Writing {name} to GameData.killedEnemies");
        GameData.Data.killedEnemies.Add(name);

        Instantiate(Resources.Load("Prefabs/Coin"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator CollisionDamageCooldown()
    {
        Debug.Log($"Collision damage cooldown for {collisionDamageCooldownTime} seconds...");
       
        collider.enabled = false;
        yield return new WaitForSeconds(collisionDamageCooldownTime);
        collider.enabled = true;
    }

    public void GetName()
    {
        throw new NotImplementedException();
    }

    public void AddItem(Item item)
    {
        usableItem = Instantiate(item, transform);
        
    }
}
