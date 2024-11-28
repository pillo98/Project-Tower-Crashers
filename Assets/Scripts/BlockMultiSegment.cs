using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMultiSegment : MonoBehaviour
{
    [SerializeField]
    private int blockHP;

    [SerializeField]
    private List<SpriteRenderer> spriteRenderer;

    [SerializeField]
    private List<Sprite> Textures;


    public void TakeDamage(int Amount)
    {
        blockHP -= Amount;
        ChangeState(blockHP);
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
                spriteRenderer[0].sprite = Textures[0];
                spriteRenderer[1].sprite = Textures[3];
                spriteRenderer[2].sprite = Textures[6];
                break;
            case 2:
                spriteRenderer[0].sprite = Textures[1];
                spriteRenderer[1].sprite = Textures[4];
                spriteRenderer[2].sprite = Textures[7];
                break;
            case 1:
                spriteRenderer[0].sprite = Textures[2];
                spriteRenderer[1].sprite = Textures[5];
                spriteRenderer[2].sprite = Textures[8];

                break;
        }
    }
}
