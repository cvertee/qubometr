using System;
using System.Collections;
using System.Linq;
using System.Text;
using Core;
using Data;
using Game;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, ITakesDamage, ICharacter
{
    public EnemyInfoSO enemyInfo;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;

    public UnityEvent onDeath;
    public bool playsAlertSound = true;
    public Vector3 moveDirection = Vector3.right;

    private float hp = 0.0f;
    private float moveSpeedMultiplier = 1.0f;
    private float currentMoveSpeed = 0.0f;
    private CharacterState state = CharacterState.Idle;
    private bool canAttack = true;
    private bool grounded = false;
    private Item usableItem;
    
    private AudioManager audioManager;
    private GameManager gameManager;
    private CoinSpawner coinSpawner;
    private GameSettingsSO gameSettings;
    private IParticleEmitter particleEmitter;

    [Inject]
    public void Init(
        AudioManager audioManager,
        GameManager gameManager,
        CoinSpawner coinSpawner,
        GameSettingsSO gameSettings,
        IParticleEmitter particleEmitter)
    {
        this.audioManager = audioManager;
        this.gameManager = gameManager;
        this.coinSpawner = coinSpawner;
        this.gameSettings = gameSettings;
        this.particleEmitter = particleEmitter;
    }
    
    private void Awake()
    {
        hp = enemyInfo.hp;
        
        enemyInfo.sightDistanceFollow = enemyInfo.sightDistance * 5.0f;
        
        if (GameData.Data.killedEnemies.Any(x => x == name))
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StartCoroutine(AIUpdate());
        
        if ((usableItem = GetComponentInChildren<Weapon>()) == null) // means enemy doesn't have any weapons
        {
            gameManager.AddItemById("Knife", this);
        }
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
            transform.position + enemyInfo.overlapCircleOffset, 
            enemyInfo.overlapCircleRadius,
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
            yield return new WaitForSecondsRealtime(gameSettings.aiTickTime);
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
        if (!boxCollider2D.enabled)
            return;
    }

    private void OnDrawGizmos()
    {
        var position = transform.position;
        
        Gizmos.DrawLine(position, position + new Vector3(enemyInfo.sightDistance * moveDirection.x, 0));
        Gizmos.DrawLine(position, position + new Vector3(enemyInfo.sightDistance * -moveDirection.x, 0));
        Gizmos.DrawLine(position + enemyInfo.obstacleDetectorPosition, position + enemyInfo.obstacleDetectorPosition + new Vector3(enemyInfo.obstacleDetectorDistance * moveDirection.x, 0));
        Gizmos.DrawSphere(position + enemyInfo.overlapCircleOffset, enemyInfo.overlapCircleRadius);
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
            enemyInfo.sightDistance, 
            LayerMask.GetMask("Player")
        );
        var playerHitLeft = Physics2D.Raycast(
            transform.position, 
            Vector2.left, 
            enemyInfo.sightDistance, 
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
        currentMoveSpeed = enemyInfo.moveSpeed;

        var position = transform.position;
        var playerRightCast = Physics2D.Raycast(
            position, 
            Vector2.right,
            enemyInfo.sightDistanceFollow, 
            LayerMask.GetMask("Player")
        );
        var playerLeftCast = Physics2D.Raycast(
            position, 
            Vector2.left,
            enemyInfo.sightDistanceFollow, 
            LayerMask.GetMask("Player")
        );
        var playerNearCast = Physics2D.Raycast(
            position, 
            moveDirection, 
            enemyInfo.attackMinDistance, 
            LayerMask.GetMask("Player")
        );
        var obstacleNearCast = Physics2D.Raycast(
            position + enemyInfo.obstacleDetectorPosition,
            moveDirection,
            enemyInfo.obstacleDetectorDistance,
            ~LayerMask.GetMask(enemyInfo.ignoredObstacleMasks)
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
        if (obstacleNearCast.collider != null && !obstacleNearCast.collider.isTrigger)
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
            enemyInfo.attackMaxDistance * enemyInfo.attackMaxDistanceMultiplier, 
            LayerMask.GetMask("Player")
        );
        var playerTooNearLeftCast = Physics2D.Raycast(
            position,
            Vector2.left,
            enemyInfo.SightDistanceForPlayerTooNear,
            LayerMask.GetMask("Player")
        );
        var playerTooNearRightCast = Physics2D.Raycast(
            position,
            Vector2.left,
            enemyInfo.SightDistanceForPlayerTooNear,
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
        yield return new WaitForSeconds(enemyInfo.attackCooldownTime * enemyInfo.attackCooldownTimeMultiplier);
        canAttack = true;
    }

    public void TakeDamage(float damage)
    {
        audioManager.PlaySound(AudioResource.SwordSlash); // OnDamage.Invoke() ?
        particleEmitter.Emit("blood", transform.position);
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
            coinSpawner.Spawn(transform.position);
        }
        Destroy(gameObject);
    }
    
    private void Jump()
    {
        rb.AddForce(new Vector2(0, enemyInfo.jumpForce), ForceMode2D.Impulse);
    }

    private IEnumerator CollisionDamageCooldown()
    {
        Debug.Log($"Collision damage cooldown for {enemyInfo.collisionDamageCooldownTime} seconds...");
       
        boxCollider2D.enabled = false;
        yield return new WaitForSeconds(enemyInfo.collisionDamageCooldownTime);
        boxCollider2D.enabled = true;
    }

    public void AddItem(Item item)
    {
        item.transform.SetParent(transform);
        usableItem = item;
    }
}
