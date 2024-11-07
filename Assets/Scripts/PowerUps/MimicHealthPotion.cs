using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicHealthPotion : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp(other);
        }
    }

    private void PickUp(Collider2D player)
    {
        playerHealth.health -= 1;

        Destroy(gameObject);
    }
}
