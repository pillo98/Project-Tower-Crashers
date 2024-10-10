using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    public Vector3 initPos;
    private Rigidbody2D rigidbody;
    private Vector3 forceAtProjectile;
    public float forceFactor;
    bool HasShot;

    public int Damage;

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
}
