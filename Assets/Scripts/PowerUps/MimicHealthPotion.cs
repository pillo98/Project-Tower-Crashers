using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicHealthPotion : MonoBehaviour
{
    public PlayerHealth playerHealth;
    [SerializeField]
    Sound pickup;
    [SerializeField]
    AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickUp(other);
        }
    }

    private void PickUp(Collider2D player)
    {
        playerHealth.health -= 2;
        audioSource.PlayOneShot(pickup.clip);
        Destroy(gameObject);
    }
}
