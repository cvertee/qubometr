using Assets.Scripts.Core;
using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, ITakesDamage
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

    private float speed = 1000f;
    private float speedMultiplier = 1.25f;
    private bool grounded = false;
    private float jumpForce = 30.0f;
    private float jumpMultiplier = 1.0f;
    private float takeDamageMultiplier = 1.0f;
    private State currentState = State.Any;
    private IInteractable interactable;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    
    private Item weapon;
    private Item shield;
    private Item armor;
    
    private List<Item> items = new List<Item>();
    public AudioClip slashSound;

    private Vector2 velocity;
    private Vector2 movementDirection;
    
    [SerializeField] private float overlapCircleRadius;
    [SerializeField] private Vector3 overlapCircleOffset;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        GameManager.Instance.AddItemById("Knife", this);
        GameManager.Instance.AddItemById("ShieldPlaceholder", this);
        GameManager.Instance.AddItemById("DefaultArmor", this);
    }

    private void Update()
    {
        if (GameData.Instance.HP <= 0)
            Die();

        if (currentState == State.Locked)
            return;

        var horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0.0f)
        {
            FlipByDegrees(180f);
        }
        else if (horizontal > 0.0f)
        {
            FlipByDegrees(360f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactable?.Interact();
        }
        if (Input.GetMouseButtonDown(0))
            weapon.Use();
        if (Input.GetMouseButtonDown(1))
             shield.Use();
        if (Input.GetMouseButtonUp(1))
             shield.StopUse();
    }

    void FixedUpdate()
    {
        if (currentState == State.Locked)
            return;

        // Check if player is grounded with overlapping circle
        var groundHit = Physics2D.OverlapCircle(
            transform.position + overlapCircleOffset, 
            overlapCircleRadius,
            LayerMask.GetMask("Floor")
        );
        grounded = groundHit != null;

        // Move 
        rb.velocity = new Vector2(
            Input.GetAxis("Horizontal") * (speed * speedMultiplier) * Time.fixedDeltaTime,
            rb.velocity.y
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var possibleInteractable = collision.GetComponent<IInteractable>();
        if (possibleInteractable != null)
        {
            Debug.Log($"found interactable {collision.name}");
            interactable = possibleInteractable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() != null)
        {
            Debug.Log($"exit interactable {collision.name}");
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
        var totalDamage = damage * takeDamageMultiplier;
        
        foreach (var item in items)
        {
            if (!item.isBeingUsed)
                continue;
            
            // probably no need for checking whether it's armor or shield
            // because default items like weapon will have no protection 
            totalDamage -= totalDamage * item.protectionMultiplier;
        }

        if (totalDamage <= 0.0f)
            totalDamage = 0.0f;
        
        if (currentState != State.Attacking) // TODO: fix this 
        {
            GameData.Instance.HP -= totalDamage;
            
            Debug.Log($"Player: Took {totalDamage} HP");
            
            if (currentState == State.Blocking)
                return; // TODO: shield particles
            
            Instantiate(Resources.Load("Prefabs/BloodParticle"), transform.position, Quaternion.identity);
        }
    }

    private void Jump()
    {
        Debug.Log($"Try jump grounded: {grounded}");

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
        weapon.Use();
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
        GameEvents.onPlayerDeath.Invoke();
        currentState = State.Locked;
        StartCoroutine(DieCooldown());
    }
    private IEnumerator DieCooldown() // TODO: use it somewhere else?
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddItem(GameObject itemGO)
    {
        var item = Instantiate(itemGO, transform)
            .GetComponent<Item>();
        
        items.Add(item);

        item.owner = this;
        
        if (item.type == ItemType.Weapon)
            weapon = item;

        if (item.type == ItemType.Shield)
            shield = item;
        
        if (item.type == ItemType.Armor)
            armor = item;
    }
}
