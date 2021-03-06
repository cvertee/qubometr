using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Zenject;

public class Player : MonoBehaviour, ITakesDamage, ICharacter
{
    enum State
    {
        Moving,
        Attacking,
        Blocking,
        Jumping,
        Locked,
        Any
    }

    private float hp = 0f;
    private float speed = 1000f;
    private float speedMultiplier = 1.25f;
    private bool grounded = false;
    private float jumpForce = 42.0f;
    private float jumpMultiplier = 1.0f;
    private float takeDamageMultiplier = 1.0f;
    private State currentState = State.Any;
    private IInteractable interactable;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    
    private Item primaryWeapon;
    private Item secondaryWeapon;
    private Item armor;
    private List<Item> items = new List<Item>();

    public AudioClip slashSound;

    private Vector2 velocity;
    private Vector2 movementDirection = Vector2.zero;
    
    [SerializeField] private float overlapCircleRadius;
    [SerializeField] private Vector3 overlapCircleOffset;
    [SerializeField] private string[] ignoredGroundMasks;

    private GameManager gameManager;
    private GameSettingsSO gameSettings;
    private IParticleEmitter particleEmitter;

    [Inject]
    public void Init(
        GameManager gameManager, 
        GameSettingsSO gameSettings,
        IParticleEmitter particleEmitter)
    {
        this.gameManager = gameManager;
        this.gameSettings = gameSettings;
        this.particleEmitter = particleEmitter;
    }
    
    private void Awake()
    {
        hp = GameData.GetHealth();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        // TODO: use something else like onPlayerLockRequested???
        GameEvents.onPopupUiElementShowed.AddListener(() => currentState = State.Locked);
        GameEvents.onPopupUiElementsEnded.AddListener(() => currentState = State.Any);

        GameEvents.onPlayerHealthModified.AddListener((health) => hp = health);
    }

    private void Start()
    {
        if (GameData.Data.playerPosition != null)
            transform.position = GameData.Data.playerPosition.ToNormalVector3();
        
        if (SceneManager.GetActiveScene().name == "Dev")
        {
            gameManager.AddItemById("Knife", this);
            gameManager.AddItemById("ShieldPlaceholder", this);
            gameManager.AddItemById("DefaultArmor", this);
        }

        foreach (var itemId in GameData.Data.playerItemIds.ToArray())
        {
            gameManager.AddItemById(itemId, this);
        }
    }

    private void Update()
    {
        if (hp <= 0)
            Die();

        if (currentState == State.Locked)
            return;
    }

    private void FixedUpdate()
    {
        if (currentState == State.Locked)
            return;

        // Check if player is grounded with overlapping circle
        var groundHit = Physics2D.OverlapCircle(
            transform.position + overlapCircleOffset, 
            overlapCircleRadius,
            ~LayerMask.GetMask(ignoredGroundMasks)
        );
        if (groundHit != null)
        {
            var groundHitCollider = groundHit.GetComponent<Collider2D>();
            if (groundHitCollider != null)
                grounded = !groundHitCollider.isTrigger; // ignore stuff like pickable, doors, etc

        }
        else
        {
            grounded = false;
        }

        // Move 
        rb.velocity = new Vector2(
            movementDirection.x * (speed * speedMultiplier) * Time.fixedDeltaTime,
            rb.velocity.y
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var possibleInteractable = collision.GetComponent<IInteractable>();
        if (possibleInteractable != null)
        {
            interactable = possibleInteractable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() != null)
        {
            interactable = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + overlapCircleOffset, overlapCircleRadius);
        
        if (grounded)
        {
            Debug.DrawRay(transform.position, new Vector3(0f, 1f), Color.cyan);
        }
    }

    public void OnJumpAction(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            Jump();
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        var inputMovement = ctx.ReadValue<Vector2>();
        
        if (inputMovement.x < 0.0f)
        {
            FlipByDegrees(180f);
        }
        else if (inputMovement.x > 0.0f)
        {
            FlipByDegrees(360f);
        }

        movementDirection = new Vector2(inputMovement.x, 0.0f);
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (primaryWeapon == null)
            return;
        
        if (ctx.started)
            primaryWeapon.Use();
    }
    
    public void OnSecondaryAttack(InputAction.CallbackContext ctx)
    {
        if (secondaryWeapon == null)
            return;
        
        if (ctx.started)
            secondaryWeapon.Use();
        else if (ctx.performed)
            secondaryWeapon.StopUse();
    }

    public void OnInteraction(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            interactable?.Interact();
    }

    private void FlipByDegrees(float degrees)
    {
        transform.eulerAngles = new Vector3(0f, -degrees, 0f);
    }

    public void Lock()
    {
        rb.velocity = new Vector2(0f, 0f); // Stop player to prevent sliding in cutscene
        currentState = State.Locked;
    }

    public void Unlock()
    {
        currentState = State.Any;
    }

    public void TakeDamage(float damage)
    {
        var totalDamage = damage / gameSettings.damageMultiplierToEnemies; // restore the original damage
        totalDamage *= gameSettings.damageReceiveMultiplier;

        foreach (var item in items)
        {
            if (!item.data.isBeingUsed)
                continue;
            
            // probably no need for checking whether it's armor or shield
            // because default items like weapon will have no protection 
            totalDamage -= totalDamage * item.data.protectionMultiplier;
        }

        if (totalDamage <= 0.0f)
            totalDamage = 0.0f;
        else
            GameEvents.onPlayerReceivedDamage.Invoke();
        
        if (currentState != State.Attacking) // TODO: fix this 
        {
            hp = GameData.DecreaseHealth(totalDamage);
            
            Debug.Log($"Player: Took {totalDamage} HP");
            
            if (currentState == State.Blocking)
                return; // TODO: shield particles
            
            if (totalDamage > 0.0f)
                particleEmitter.Emit("blood", transform.position);
        }
    }

    private void Jump()
    {
        if (!grounded)
            return;

        rb.AddForce(new Vector2(0, jumpForce * jumpMultiplier), ForceMode2D.Impulse);
    }
    
    private void Attack()
    {
        if (currentState == State.Attacking || currentState == State.Blocking)
            return;

        currentState = State.Attacking;
        audioSource.PlayOneShot(slashSound);
        primaryWeapon.Use();
        StartCoroutine(AttackCooldown());
    }
    private IEnumerator AttackCooldown() // TODO: fix timings?
    {
        yield return new WaitForSeconds(0.15f);
        currentState = State.Any;

        yield return new WaitForSeconds(0.15f);
    }

    private void Die()
    {
        currentState = State.Locked;
        StartCoroutine(DieCooldown());
    }
    private IEnumerator DieCooldown() // TODO: use it somewhere else?
    {
        yield return new WaitForSeconds(1.0f);
        GameEvents.onPlayerDeath.Invoke();
    }

    public void AddItem(Item item)
    {
        // item = Instantiate<Item>(item, transform);
        item.name = item.data.name;
        item.transform.SetParent(transform);
        item.transform.localPosition = new Vector3(0, 0, -2); // Fixes pretty weird bug
        items.Add(item);
        GameData.Data.playerItemIds.Add(item.name);

        // item.owner = this;

        if (item.data.type == ItemType.Weapon)
        {
            if (primaryWeapon != null)
                Destroy(primaryWeapon.gameObject);
            
            primaryWeapon = item;
        }

        if (item.data.type == ItemType.Shield)
        {
            if (secondaryWeapon != null)
                Destroy(secondaryWeapon.gameObject);
            
            secondaryWeapon = item;
        }

        if (item.data.type == ItemType.Armor)
        {
            if (armor != null)
                Destroy(armor.gameObject);
            
            armor = item;
        }
    }
}
