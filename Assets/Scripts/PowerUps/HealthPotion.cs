using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public PlayerHealth playerHealth;
    HealthHeartBar healthHeartBar;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            playerHealth.health += 2f;
            
            Destroy(gameObject);
        }
    }
}
