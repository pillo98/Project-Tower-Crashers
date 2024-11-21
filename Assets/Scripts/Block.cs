using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private int blockHP;

    [SerializeField] 
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private List<Sprite> Textures;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SlingShot slingShot = collision.gameObject.GetComponent<SlingShot>();

        if (collision.gameObject.tag == "Projectile")
        {
            blockHP -= slingShot.BuildDamage;
            ChangeState(blockHP);
        }
    }

    private void Update()
    {
        if (blockHP <= 0)
        {
            Destroy(gameObject);

        }
    }


    private void ChangeState(int state)
    {
        switch (state)
        {
            case 0:
                Destroy(gameObject);
                break;
            case 3:
                spriteRenderer.sprite = Textures[0];
                break;
            case 2:
                spriteRenderer.sprite = Textures[1];
                break;
            case 1:
                spriteRenderer.sprite = Textures[2];
                break;
        }
    }
}
