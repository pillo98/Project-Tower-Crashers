using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float speed;
    private float valueX;

    public bool fallTrough;

    private void Update()
    {
        Keyboard myKeyboard = Keyboard.current;
        valueX = 0;
        if (myKeyboard != null)
        {
            if (myKeyboard.aKey.isPressed)
            {
                valueX = - 1;
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

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(valueX * speed, rb2d.velocity.y);
    }
}
