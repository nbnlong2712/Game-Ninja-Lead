using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Bullet : MonoBehaviour
{
    [SerializeField] float speed = 35f;
    [SerializeField] float timeToDestroy = 2f;
    [SerializeField] GameObject explosion;
    [SerializeField] int damage = 2;
    BoxCollider2D boxCollider2D;
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        Invoke("DestroyBullet", timeToDestroy);
    }

    void Update()
    {
        //Vector2.up nghĩa là hướng nào thì bắn thẳng hướng đó
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if(boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Shield")))
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            DestroyBullet();
        }
        if (collision.tag == "Shield")
        {
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
