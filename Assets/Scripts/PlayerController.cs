using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float MOVESPEED = 5f;
    private Rigidbody2D rb;
    private Vector3 moveDir;
    private Vector3 flipSprite;

    private Animator animator;

    private bool canMove = true;
    [SerializeField] private bool facingRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        flipSprite = new Vector3(0, 180, 0);
    }

    void Update()
    {
        HandleMovment();
    }

    private void HandleMovment()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (canMove)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                moveY = +1f;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                moveY = -1f;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                moveX = -1f;
                Debug.Log("I am walking left");

                if (facingRight == false)
                {
                    FlipSprite();
                }
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                moveX = +1f;
                Debug.Log("I am walking right");

                if (facingRight)
                {
                    FlipSprite();
                }
            }

            moveDir = new Vector3(moveX, moveY, 0).normalized;

            animator.SetFloat("Speed", moveDir.magnitude);
        }
    }

    private void FlipSprite()
    {
        facingRight = !facingRight; // this flips whatever the var is set to when it gets called
        transform.Rotate(0, 180, 0);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + moveDir * MOVESPEED * Time.deltaTime);
    }
}
