using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private int blockHP;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SlingShot slingShot = collision.gameObject.GetComponent<SlingShot>();

        if (collision.gameObject.tag == "Projectile")
        {
            blockHP -= slingShot.Damage;
        }
    }

    private void Update()
    {
        if (blockHP <= 0)
        {
            Destroy(gameObject);

        }
    }
}
