using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    MovementController playerControls;
    PlatformEffector2D effector;

    private void Awake()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) 
        {
            playerControls = collision.gameObject.GetComponent<MovementController>();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (playerControls == null)
            return;
        if (playerControls.fallTrough)
        {
            effector.rotationalOffset = 180;
            playerControls = null;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerControls = null;
        effector.rotationalOffset = 0;
    }

}
