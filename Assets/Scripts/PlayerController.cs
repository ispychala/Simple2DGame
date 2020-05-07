using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject deathEffect;
    public GameObject bullet;

    public GameManager gm;
    public AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip deathSound;
    public AudioClip throwSound;


    Vector2 movement;
    Vector2 startTouch, swipeDelta;

    void Update()
    {
        CheckSwipe();

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void CheckSwipe()
    {
        #region standalone
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if ((Vector2)Input.mousePosition == startTouch)
            {
                Shoot();
            }
            else
            {
                Walk((Vector2)Input.mousePosition);
            }
        }
        #endregion

        #region mobile
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                if (Input.touches[0].position == startTouch)
                {
                    Shoot();
                }
                else
                {
                    Walk(Input.touches[0].position);
                }
            }
        }
        #endregion
    }


    void Shoot()
    {
        GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
        Bullet bscript = b.GetComponent<Bullet>();
        bscript.movement = movement;
        bscript.moveSpeed = moveSpeed * 3;
        audioSource.PlayOneShot(throwSound, 1f);
    }

    void Walk(Vector2 endTouch)
    {
        swipeDelta = endTouch - startTouch;
        float x = swipeDelta.x;
        float y = swipeDelta.y;

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x < 0)
            { //swipeLeft
                movement.x = -1;
                movement.y = 0;
            }
            else
            { //swipeRight
                movement.x = 1;
                movement.y = 0;
            }
        }
        else
        {
            if (y < 0)
            {//swipeDown
                movement.x = 0;
                movement.y = -1;
            }
            else
            {//swipeUp
                movement.x = 0;
                movement.y = 1;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        movement *= -1;

        if (collision.gameObject.tag == "Enemy")
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
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        gm.OnPlayerDead();
        Destroy(gameObject);
    }

}
