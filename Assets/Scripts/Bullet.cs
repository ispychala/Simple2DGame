using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 movement;
    public float moveSpeed = 3f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);  
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
