using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField] private float cooldownTime = 5f;
    [SerializeField] private float lastUsedTime;

    private void Awake()
    {
        lastUsedTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastUsedTime >= cooldownTime)
        {
            // Explode the object
            Destroy(gameObject);
        }
    }
}
