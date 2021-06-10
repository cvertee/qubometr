using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, ITakesDamage, ICharacter
{
    private Rigidbody2D rb;
    private BoxCollider2D collider;

    public UnityEvent onDeath;
    
    public float hp = 10;

    public bool playsAlertSound = true;
    
    public float moveSpeed = 10.0f;
    private float moveSpeedMultiplier = 1.0f;
    private float currentMoveSpeed = 0.0f;
    
    public float attackCooldownTime = 0.5f;
    public float attackCooldownTimeMultiplier = 1.0f;
    public float attackMinDistance = 3.0f;
    public float attackMaxDistance = 3.0f;
    public float attackMaxDistanceMultiplier = 1.0f;
    
    public float collisionDamageCooldownTime = 1.0f; // Time in which box collider of enemy is disabled
    
    public float jumpForce = 40.0f;
    
    public Vector3 wallDetectionBoxSize;
    public Vector3 wallDetectionBoxOffset;
    
    [SerializeField] private float overlapCircleRadius;
    [SerializeField] private Vector3 overlapCircleOffset;
    
    public Vector3 moveDirection = Vector3.right;

    [SerializeField] private float sightDistance = 10.0f;
    private float sightDistanceFollow;
    private const float SightDistanceForWall = 2.0f;
    private const float SightDistanceForPlayerTooNear = 0.4f;

    private CharacterState state = CharacterState.Idle;
    private bool canAttack = true;
    private bool grounded = false;
    private Item usableItem;
    
    private void Awake()
    {
        sightDistanceFollow = sightDistance * 5.0f;
        
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
        
        //AIStateCheck();

        if (moveDirection == Vector3.left)
        {
            transform.eulerAngles = new Vector3(0f, -180f);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f);
        }
        
        var groundHit = Physics2D.OverlapCircle(
            transform.position + overlapCircleOffset, 
            overlapCircleRadius,
            ~LayerMask.GetMask("Player")
        );
        grounded = groundHit != null;
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
            yield return new WaitForSecondsRealtime(GameSettings.GlobalAiTickTime);
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
    }

    private void OnDrawGizmos()
    {
        var position = transform.position;
        
        Gizmos.DrawLine(position, position + new Vector3(sightDistance * moveDirection.x, 0));
        Gizmos.DrawLine(position, position + new Vector3(sightDistance * -moveDirection.x, 0));
    }

    private void OnDrawGizmosSelected()
    {
        var position = transform.position;
        
        Gizmos.DrawCube(position + wallDetectionBoxOffset, wallDetectionBoxSize);
        Gizmos.DrawSphere(position + overlapCircleOffset, overlapCircleRadius);
    }

    private void OnGUI()
    {
        #if DEBUG
        var cam = Camera.main;
        var pos = cam.WorldToScreenPoint(transform.position);
        var sb = new StringBuilder();

        sb.AppendLine($"state={state}");

        GUI.Label(new Rect(pos.x, pos.y, 400, 400), sb.ToString());
        #endif
    }

    /// <summary>
    /// Stands and waits for player at two sides
    /// </summary>
    protected virtual void OnIdle()
    {
        currentMoveSpeed = 0.0f;

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
            
            if (playsAlertSound)
                GameEvents.onEnemyAlert.Invoke();
            Debug.Log($"Player detected by {name}. Starting to follow him");
        }
    }
    
    /// <summary>
    /// Follows player
    /// </summary>
    protected virtual void OnFollow()
    {
        currentMoveSpeed = moveSpeed;

        var position = transform.position;
        var playerRightCast = Physics2D.Raycast(
            position, 
            Vector2.right,
            sightDistanceFollow, 
            LayerMask.GetMask("Player")
        );
        var playerLeftCast = Physics2D.Raycast(
            position, 
            Vector2.left,
            sightDistanceFollow, 
            LayerMask.GetMask("Player")
        );
        var playerNearCast = Physics2D.Raycast(
            position, 
            moveDirection, 
            attackMinDistance, 
            LayerMask.GetMask("Player")
        );
        var wallNearCast = Physics2D.BoxCast(
            position + wallDetectionBoxOffset,
            wallDetectionBoxSize,
            0f,
            moveDirection,
            SightDistanceForWall,
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
            Jump();
        }
    }

    /// <summary>
    /// Attacks player
    /// </summary>
    protected virtual void OnAttack()
    {
        if (canAttack && usableItem != null)
        {
            currentMoveSpeed = 0f; // Stop before the attack
            usableItem.Use();
            StartCoroutine(AttackCooldown());
        }

        var position = transform.position;
        var playerHit = Physics2D.Raycast(
            position, 
            moveDirection, 
            attackMaxDistance * attackMaxDistanceMultiplier, 
            LayerMask.GetMask("Player")
        );
        var playerTooNearLeftCast = Physics2D.Raycast(
            position,
            Vector2.left,
            SightDistanceForPlayerTooNear,
            LayerMask.GetMask("Player")
        );
        var playerTooNearRightCast = Physics2D.Raycast(
            position,
            Vector2.left,
            SightDistanceForPlayerTooNear,
            LayerMask.GetMask("Player")
        );

        if (playerHit.collider == null) // means enemy lost the player
        {
            state = CharacterState.Follow;
        }
    }

    /// <summary>
    /// Blocks ability to attack for some time
    /// </summary>
    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldownTime * attackCooldownTimeMultiplier);
        canAttack = true;
    }

    public void TakeDamage(float damage)
    {
        GameEvents.onAudioNamePlayRequested.Invoke("slashkut"); // OnDamage.Invoke() ?
        Instantiate(Resources.Load("Prefabs/BloodParticle"), transform.position, Quaternion.identity);
        hp -= damage; //TODO: use damage var
    }

    private void Die()
    {
        Debug.Log($"Writing {name} to GameData.killedEnemies");
        GameData.Data.killedEnemies.Add(name);
        GameData.Data.killedEnemiesCount += 1;
        
        onDeath?.Invoke();

        for (var i = 0; i < 15; i++)
        {
            // TODO: USE POOL!!!!!!!!!!!
            Instantiate(Resources.Load("Prefabs/Coin"), transform.position + new Vector3(Random.Range(0, 7), 0), Quaternion.identity);
        }
        Destroy(gameObject);
    }
    
    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private IEnumerator CollisionDamageCooldown()
    {
        Debug.Log($"Collision damage cooldown for {collisionDamageCooldownTime} seconds...");
       
        collider.enabled = false;
        yield return new WaitForSeconds(collisionDamageCooldownTime);
        collider.enabled = true;
    }

    public void AddItem(Item item)
    {
        usableItem = Instantiate(item, transform);
    }
}
