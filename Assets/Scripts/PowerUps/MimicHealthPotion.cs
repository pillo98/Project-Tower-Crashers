using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicHealthPotion : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.health -= 1f;

            Destroy(gameObject);
        }
    }
}
