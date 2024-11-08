using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private Transform PickUpPoint;
    private Transform player;

    public float pickUpDistance;
    public float forceMultiplier;

    public bool readyToThrow;
    public bool itemIsPicked;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        PickUpPoint = GameObject.Find("PickUpPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && itemIsPicked == true && readyToThrow)
        {
            forceMultiplier += 300 * Time.deltaTime;
        }

        pickUpDistance = Vector2.Distance(player.position, transform.position);

        if (pickUpDistance <= 2)
        {
            if (Input.GetKeyDown(KeyCode.E) && itemIsPicked == false && PickUpPoint.childCount < 1)
            {
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<CircleCollider2D>().enabled = false;
                this.transform.position = PickUpPoint.position;
                this.transform.parent = GameObject.Find("PickUpPoint").transform;

                itemIsPicked = true;
                forceMultiplier = 0;

            }
        }

        if (Input.GetKeyUp(KeyCode.E) && itemIsPicked == true)
        {
            readyToThrow = true;

            if (forceMultiplier > 10 )
            {
                rb2d.AddForce(player.transform.forward * forceMultiplier);
                this.transform.parent = null;
                GetComponent<Rigidbody2D>().gravityScale = 1;
                GetComponent<CircleCollider2D>().enabled = true;
                itemIsPicked = false;

                forceMultiplier = 0;
                readyToThrow = false;
            }
            forceMultiplier = 0;
        }
    }
}
