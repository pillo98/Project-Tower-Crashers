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
        if (other.gameObject.CompareTag("P1")|| other.gameObject.CompareTag("P2"))
        {
            PickUp(other);
        }
    }

    private void PickUp(Collider2D player)
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(2);
        audioSource.PlayOneShot(pickup.clip);
        Destroy(gameObject);
    }
}
