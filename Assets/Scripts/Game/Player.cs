using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 1000f;
    private bool grounded = false;
    private bool canAttack = true;
    private bool canDash = true;

    private Rigidbody2D rb;
    public GameObject weapon;

    private Vector2 velocity;
    private Vector2 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, -2.5f), Color.red);
        var groundHit = Physics2D.Linecast(transform.position, transform.position + new Vector3(0, -2.5f));
        if (groundHit.collider == null)
        {
            grounded = false;
        }
        else
        {
            grounded = true;
        }

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime, rb.velocity.y);
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
        if (!canAttack)
            return;

        weapon.SetActive(true);
        StartCoroutine(AttackCooldown());
        canAttack = false;
    }
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(0.15f);
        weapon.SetActive(false);
        yield return new WaitForSeconds(0.15f);
        canAttack = true;
    }
}
