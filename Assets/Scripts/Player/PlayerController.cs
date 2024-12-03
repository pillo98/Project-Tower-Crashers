using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController : MonoBehaviour
{

    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    private Animator animator;
    Rigidbody2D rb2d;
    public float jumpPower;
    public float speed;
    private float moveInput;
    private float valueX;

    public bool fallTrough;
    private bool isGrounded;
    public Transform groundCheck;

    public float health, maxHealth;
    private bool facingRight = false;
    public bool canMove = false;

    [SerializeField]
    private Sound Death;
    [SerializeField]
    private Sound Jump;
    [SerializeField]
    private Sound Damage;

    [SerializeField]
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        CheckGround();
        rb2d.velocity = new Vector2(valueX * speed, rb2d.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard myKeyboard = Keyboard.current;
        valueX = 0;
        if (myKeyboard != null && canMove)
        {
            if (myKeyboard.aKey.isPressed)
            {
                valueX = -1;
            }
            if (myKeyboard.dKey.isPressed)
            {
                valueX = 1;
            }
            if (myKeyboard.aKey.isPressed && myKeyboard.dKey.isPressed)
            {
                valueX = 0;
            }
        }

        // Set player state for running or idle based on movement and grounded state
        if (isGrounded)
        {
            if (valueX != 0)
            {
                animator.SetInteger("playerState", 1); // Running
            }
            else
            {
                animator.SetInteger("playerState", 0); // Idle
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded && canMove)
        {
            audioSource.PlayOneShot(Jump.clip);
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
        }
        // Set jump animation if the player is airborne
        if (!isGrounded)
        {
            animator.SetInteger("playerState", 2); // Jumping
        }

        if (facingRight == false && valueX > 0)
        {
            Flip();
        }
        else if (facingRight == true && valueX < 0)
        {
            Flip();
        }
        // ---- ONE WAY PLATFORMER ----
        if (Keyboard.current.sKey.isPressed)
        {
            fallTrough = true;
        }
        else
        {
            fallTrough = false;
        }
    }
    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, 0.2f);
        isGrounded = colliders.Length > 1;
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
