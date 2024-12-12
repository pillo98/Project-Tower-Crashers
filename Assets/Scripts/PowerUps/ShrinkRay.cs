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

    private void PickUp(Collider2D player)
    {
        player.transform.localScale /= multiplier;
        audioSource.PlayOneShot(pickup.clip);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 9)
        {
            PickUp(other.collider);
        }
    }
}
