using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
    public Vector3 leftEnd;
    public Vector3 rightEnd;
    public Vector3 direction = Vector3.left;
    public float speed;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (transform.position.x < leftEnd.x)
            direction = Vector3.right;
        else if (transform.position.x > rightEnd.x)
            direction = Vector3.left;
    }
}
