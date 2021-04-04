using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{
    public float speed = 10.0f;
    
    void Update()
    {
        transform.position += new Vector3(-Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);
    }
}
