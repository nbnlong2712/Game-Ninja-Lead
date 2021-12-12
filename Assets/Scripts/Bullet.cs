using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 55f;
    [SerializeField] float timeToDestroy = 2f;
    [SerializeField] GameObject explosion;
    [SerializeField] int damage = 10;
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
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
