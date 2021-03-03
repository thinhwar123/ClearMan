using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletData baseData;
    [SerializeField] private float duration;
    private int curBounce;
    private Rigidbody2D rb;
    private Vector3 lastDirection;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        curBounce = 0;
    }
    public void Start()
    {
        StartCoroutine(StopTravel());
    }
    public void Update()
    {
        lastDirection = rb.velocity;
    }
    public void Travel(Vector3 direction)
    {
        rb.velocity = direction.normalized * baseData.bulletSpeed;

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (curBounce < baseData.maxBounce)
        {
            Vector2 direction = Vector2.Reflect(lastDirection.normalized, collision.contacts[0].normal);
            Travel(direction);
            curBounce++;
        }
        else
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
            rb.velocity = Vector2.zero;
            Destroy(gameObject, GetComponent<TrailRenderer>().time);
        }

    }
    IEnumerator StopTravel()
    {
        yield return new WaitForSeconds(duration);
        GetComponent<CircleCollider2D>().isTrigger = true;
        rb.velocity = Vector2.zero;
        Destroy(gameObject, GetComponent<TrailRenderer>().time);
    }
}
