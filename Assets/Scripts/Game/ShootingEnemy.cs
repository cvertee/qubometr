using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class ShootingEnemy : Enemy
{
    public GameObject bullet;

    private void Start()
    {
    }

    protected override void OnAttack()
    {
        if (!canAttack)
            return;

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

        Instantiate(bullet, transform);

        StartCoroutine(ShootCooldown());
    }

    IEnumerator ShootCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldownTime * attackCooldownTimeMultiplier);
        canAttack = true;
    }
}