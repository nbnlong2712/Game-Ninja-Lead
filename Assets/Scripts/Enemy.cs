using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health = 50;
    [SerializeField] protected float speed;
    [SerializeField] protected float timeBetweenAttack;
    [SerializeField] protected float damage = 15;

    public virtual void Start()
    {
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Invoke("DestroyEnemy", 2);
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}