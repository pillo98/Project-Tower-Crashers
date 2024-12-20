using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    PlayerController playerControls;
    PlatformEffector2D effector;

    private void Awake()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>()) 
        {
            playerControls = collision.gameObject.GetComponent<PlayerController>();
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
