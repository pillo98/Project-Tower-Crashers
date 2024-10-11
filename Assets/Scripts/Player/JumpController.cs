using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{

    Rigidbody2D rb2d;
    [SerializeField] int jumpPower;

    //----GROUNDCHECK----
    //public Transform groundCheck;
    //public LayerMask groundLayer;
    //bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1.0f, 0.3f), CapsuleDirection2D.Horizontal, 0, groundLayer);

        if (Input.GetButtonDown("Jump") /*&& isGrounded*/)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
        }
    }
}
