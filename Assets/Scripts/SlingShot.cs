using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;

public class SlingShot : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    public Vector3 initPos;
    private Rigidbody2D rigidbody;
    private Vector3 forceAtProjectile;
    public float forceFactor;
    public bool HasShot;

    public int BuildDamage;

    public int damage = 2;
    public string TagName;

    [SerializeField]
    float Lifetime;
    float SinceShot;

    public GameObject trajectoryDot;
    private GameObject[] trajectoryDots;
    public int number;



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
            Destroy(gameObject);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == TagName)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
        }
        if (collision.gameObject.layer == 8)
        {
            if(collision.gameObject.GetComponent<Block>())
            {
                collision.gameObject.GetComponent<Block>().TakeDamage(BuildDamage);
            }
            if (collision.gameObject.GetComponent<BlockMultiSegment>())
            {
                collision.gameObject.GetComponent<BlockMultiSegment>().TakeDamage(BuildDamage);
            }
            if (collision.gameObject.GetComponent<BlockTwoSegment>())
            {
                collision.gameObject.GetComponent<BlockTwoSegment>().TakeDamage(BuildDamage);
            }
        }
    }
}
