using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.GetComponent<ITakesDamage>();
        damageable?.TakeDamage(0); // TODO: replace?
    }

    public void Attack()
    {
        GetComponent<Animator>().Play("Attack");
    }
}
