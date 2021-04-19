using System;
using Assets.Scripts.Core;
using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    enum State
    {
        Moving,
        Attacking,
        Jumping,
        Any
    }

    private float speed = 1000f;
    private float speedMultiplier = 1.25f;
    private bool grounded = false;
    private bool canAttack = true;
    private bool canDash = true;
    private bool canMove = true;
    private State currentState = State.Any;
    private IInteractable interactable;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private Weapon weapon;
    public GameObject weaponGO;
    public AudioClip slashSound;

    private Vector2 velocity;
    private Vector2 movementDirection;
    
    [SerializeField] private float overlapCircleRadius;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        if (GameData.Instance.HP <= 0)
            Die();

        if (!canMove)
            return;

        var horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0.0f)
        {
            movementDirection = Vector2.left;
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
        }
        else if (horizontal > 0.0f)
        {
            movementDirection = Vector2.right;
            transform.eulerAngles = new Vector3(0f, -360f, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"Try jump grounded: {grounded}");

            if (!grounded)
                return;

            rb.AddForce(new Vector2(0, 30f), ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactable == null)
                return;

            interactable.Interact();
        }

        if (Input.GetMouseButtonDown(1))
            Dash();
        if (Input.GetMouseButtonDown(0))
            Attack();

        if (grounded)
        {
            Debug.DrawRay(transform.position, new Vector3(0f, 1f), Color.cyan);
        }
    }

    void FixedUpdate()
    {
        if (!canMove)
            return;

        // Check if player is grounded with overlapping circle
        var groundHit = Physics2D.OverlapCircle(transform.position, overlapCircleRadius);
        if (groundHit == null)
        {
            grounded = false;
        }
        else
        {
            grounded = true;
        }

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

        if (collision.CompareTag("Enemy"))
        {
            ReceiveDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            ReceiveDamage();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() != null)
        {
            Debug.Log($"exit interactable {collision.name}");
            //interactable.StopInteract();
            interactable = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, overlapCircleRadius);
    }

    public void Lock()
    {
        canMove = false;
    }

    public void Unlock()
    {
        canMove = true;
    }

    private void ReceiveDamage()
    {
        if (currentState != State.Attacking) // TODO: fix this 
        {
            GameData.Instance.HP -= 10;
            Instantiate(Resources.Load("Prefabs/BloodParticle"), transform.position, Quaternion.identity);
        }
    }

    private void Dash()
    {
        return;
        if (!canDash)
            return;

        rb.MovePosition(new Vector2(transform.position.x + 9f * movementDirection.x, 0f));
        canDash = false;
        StartCoroutine(DashCooldown());
    }
    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(0.33f);
        canDash = true;
    }

    private void Attack()
    {
        currentState = State.Attacking;

        if (!canAttack)
            return;

        audioSource.PlayOneShot(slashSound);
        //weapon.SetActive(true);
        weapon.Attack();
        StartCoroutine(AttackCooldown());
        canAttack = false;
    }
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.15f);
        //weapon.SetActive(false);
        currentState = State.Any;

        yield return new WaitForSeconds(0.15f);
        canAttack = true;
    }

    public void Die()
    {
        GameEvents.onPlayerDeath.Invoke();
        canMove = false;
        StartCoroutine(DieCooldown());
    }
    private IEnumerator DieCooldown()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
