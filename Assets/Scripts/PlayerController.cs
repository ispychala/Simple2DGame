using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    bool goUp, goDown, goRight, goLeft;
    bool shooting;
    Vector3 goToPosition;

    private Vector2 desiredPosition, currentPosition;
    bool swipeUp, swipeDown, swipeLeft, swipeRight;
    Vector2 startTouch, swipeDelta;
    bool isDragging = false;
    float distCovered, timer;
    Vector3 delta;

    private void Start()
    {
        
    }

    void Update()
    {
        CheckSwipe();

        if (swipeUp)
        {
            movement.x = 0;
            movement.y = 1;
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else if (swipeDown)
        {
            movement.x = 0;
            movement.y = -1;
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", -1);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else if (swipeLeft)
        {
            movement.x = -1;
            movement.y = 0;
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else if (swipeRight)
        {
            movement.x = 1;
            movement.y = 0;
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Vertical", 0);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else if (shooting)
        {
            //Debug.Log("shoot");
            shooting = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void CheckClick()
    {
        shooting = false;

        #region standalone
        if (Input.GetMouseButtonDown(0))
        {
            goToPosition = Input.mousePosition;
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(goToPosition);
            clickPosition.z = 0;
            movement = clickPosition;
            transform.position = clickPosition;
        }
        #endregion

        #region mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
            movement = touchPosition;

            transform.position = touchPosition;

        }
        #endregion
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    void CheckSwipe()
    {
        swipeUp = swipeDown = swipeLeft = swipeRight = false;

        #region standalone
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
        }
        #endregion

        #region mobile
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDragging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }
        #endregion

        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        if (swipeDelta.magnitude > 100)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0) swipeLeft = true;
                else swipeRight = true;
            }
            else
            {
                if (y < 0) swipeDown = true;
                else swipeUp = true;
            }

            Reset();
        }
        else
        {
            shooting = true;
        }
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        movement *= -1;

        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this);
        }
    }

}
