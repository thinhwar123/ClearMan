using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private int maxBounce;
    [SerializeField] private int curBounce;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector3 lastDirection;
    [SerializeField] private float maxSpeed;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        curBounce = 0;
    }
    public void Update()
    {
        lastDirection = rb.velocity;
    }
    public void Travel(Vector3 direction, float speed, float maxSpeed)
    {
        this.maxSpeed = maxSpeed;
        rb.velocity = direction.normalized * speed;
        StartCoroutine(StopTravel());
    }
    public void Travel(Vector3 direction, float speed)
    {
        rb.velocity = direction.normalized * speed;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (curBounce < maxBounce)
        {
            Vector2 direction = Vector2.Reflect(lastDirection.normalized, collision.contacts[0].normal);
            Travel(direction, maxSpeed);
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
