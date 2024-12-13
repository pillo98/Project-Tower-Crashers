using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public PlayerHealth playerHealth;
    HealthHeartBar healthHeartBar;
    [SerializeField]
    Sound pickup;
    [SerializeField]
    AudioSource audioSource;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9) 
        {
            if (collision.gameObject.GetComponent<PlayerHealth>())
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth.health < playerHealth.maxHealth)
                {
                    playerHealth.TakeDamage(-2);
                    audioSource.PlayOneShot(pickup.clip);
                    Destroy(gameObject);
                }
            }

        }
    }
}
