﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector2 movement;
    Vector2 position;
    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject deathEffect;
    Vector2[] directions = { new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 0), new Vector2(-1, 0) }; //up, down, right, left
    int movementDir;
    public float walkTime = 2f;

    public GameManager gm;
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip deathSound;


    // Update is called once per frame
    void Update()
    {
        if (walkTime <= 0)
        {
            walkTime = Random.Range(0.5f, 3f);
            movementDir = Random.Range(0, directions.Length);
            movement = directions[movementDir];
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else
        {
            walkTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        movement *= -1;
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            Die();
        }
        else
        {
            audioSource.PlayOneShot(hitSound, 0.5f);
        }
    }

    void Die()
    {
        audioSource.PlayOneShot(deathSound, 1f);
        movement = Vector2.zero;
        Instantiate(deathEffect, transform.position, Quaternion.identity, transform.parent);
        gm.OnEnemyDead();
        Destroy(gameObject);
    }
}
