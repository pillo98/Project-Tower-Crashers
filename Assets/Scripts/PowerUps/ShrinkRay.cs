using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkRay : MonoBehaviour
{
    public float multiplier = 1.4f;
    [SerializeField]
    Sound pickup;
    [SerializeField]
    AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("P1") || other.gameObject.CompareTag("P2"))
        {
            PickUp(other);
        }
    }

    private void PickUp(Collider2D player)
    {
        player.transform.localScale /= multiplier;
        audioSource.PlayOneShot(pickup.clip);
        Destroy(gameObject);
    }
}
