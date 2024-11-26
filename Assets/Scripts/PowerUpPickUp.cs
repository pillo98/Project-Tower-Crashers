using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{
    [SerializeField]
    private string powerUpName;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerHealth>())
        {
            PlayerHealth pHP = collision.collider.GetComponent<PlayerHealth>();
            if (pHP != null)
            {
                pHP.powerUpManager.SetPowerUpOn(powerUpName);
                Destroy(gameObject);
            }
        }
    }
}


