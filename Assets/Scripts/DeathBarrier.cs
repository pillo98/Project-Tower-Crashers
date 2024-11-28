using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    [SerializeField]
    GameObject P1RespawnPoint;
    [SerializeField]
    GameObject P2RespawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P1"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(4);
            collision.transform.position = P1RespawnPoint.transform.position;

        }

        if (collision.CompareTag("P2"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(4);
            collision.transform.position = P2RespawnPoint.transform.position;

        }
    }
}
