using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float fieldOfImpact;

    public float force;

    public LayerMask LayerToHit;


    private Vector3 startPos;
    private Vector3 endPos;
    public Vector3 initPos;
    private Rigidbody2D rigidbody;
    private Vector3 forceAtProjectile;
    public float forceFactor;
    public bool HasShot;

    [SerializeField]
    GameObject explosionPrefab;

    public int BuildDamage;

    public int damage = 2;
    public string TagName;

    [SerializeField]
    float Lifetime;
    float SinceShot;

    public GameObject trajectoryDot;
    private GameObject[] trajectoryDots;
    public int number;

    public Collider2D[] objects;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != TagName && collision.gameObject.layer == 9)
        {
        
        }
        else
        {
            explode();
        }
    }


    private void explode()
    {
        objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, LayerToHit);

        foreach (Collider2D obj in objects) 
        {
            if (obj.GetComponent<Rigidbody2D>())
            {
                Vector2 direction = obj.transform.position - transform.position;
                if (obj.gameObject.layer == 8)
                 obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
                if (obj.gameObject.layer == 9 && obj.gameObject.tag == TagName )
                {
                    Debug.Log("player hit");
                    PlayerHealth playerHealth = obj.gameObject.GetComponent<PlayerHealth>();
                    playerHealth.TakeDamage(damage);
                    
                }
                if (obj.gameObject.layer == 8 && obj.gameObject.tag == TagName)
                {
                    if (obj.gameObject.GetComponent<Block>())
                    {
                        obj.gameObject.GetComponent<Block>().TakeDamage(BuildDamage);
                    }
                    if (obj.gameObject.GetComponent<BlockMultiSegment>())
                    {
                        obj.gameObject.GetComponent<BlockMultiSegment>().TakeDamage(BuildDamage);
                    }
                    if (obj.gameObject.GetComponent<BlockTwoSegment>())
                    {
                        obj.gameObject.GetComponent<BlockTwoSegment>().TakeDamage(BuildDamage);
                    }

                }


            }
        }

        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,fieldOfImpact);
    }




    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        trajectoryDots = new GameObject[number];
        initPos = transform.position;
        HasShot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (HasShot != true)
        {
            Shoot();
            SinceShot = Time.time;
        }
        if (HasShot == true && SinceShot + Lifetime <= Time.time)
        {
            explode();
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        { //click
            startPos = gameObject.transform.position;
            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i] = Instantiate(trajectoryDot, gameObject.transform);
            }

        }
        if (Input.GetMouseButton(0))
        { //drag
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            rigidbody.gravityScale = 0;
            rigidbody.velocity = Vector2.zero;
            gameObject.transform.position = initPos;
            forceAtProjectile = endPos - startPos;
            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i].transform.position = calculatePosition(i * 0.1f);
            }
        }
        if (Input.GetMouseButtonUp(0))
        { //leave
            rigidbody.gravityScale = 1;
            rigidbody.velocity = new Vector2(-forceAtProjectile.x * forceFactor, -forceAtProjectile.y * forceFactor);
            for (int i = 0; i < number; i++)
            {
                Destroy(trajectoryDots[i]);
                HasShot = true;
            }
        }
    }

    private Vector2 calculatePosition(float elapsedTime)
    {
        return new Vector2(endPos.x, endPos.y) + //X0
                new Vector2(-forceAtProjectile.x * forceFactor, -forceAtProjectile.y * forceFactor) * elapsedTime + //ut
                0.5f * Physics2D.gravity * elapsedTime * elapsedTime;
    }
}
