using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    [SerializeField]
    public PowerUpManager powerUpManager;

    public float health, maxHealth;

    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    Sound damage, death;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void OnSceneUnloaded()
    {
        OnPlayerDamaged = null;
        OnPlayerDeath = null;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        OnPlayerDamaged?.Invoke();

        if (health <= 0)
        {
            audioSource.PlayOneShot(death.clip);
            health = 0;
            Debug.Log("You are DEAD!");
            OnPlayerDeath?.Invoke();
        }
        else
        {
            audioSource.PlayOneShot(damage.clip);
        }
    }
}
