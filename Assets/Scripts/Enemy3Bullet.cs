using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Bullet : MonoBehaviour
{
    [SerializeField] float speed = 35f;
    [SerializeField] float timeToDestroy = 2f;
    [SerializeField] GameObject explosion;
    [SerializeField] int damage = 50;
    void Start()
    {
        Invoke("DestroyBullet", timeToDestroy);
    }

    void Update()
    {
        //Vector2.up nghĩa là hướng nào thì bắn thẳng hướng đó
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(damage);
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
