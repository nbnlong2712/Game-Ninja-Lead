using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected float timeBetweenAttack;
    [SerializeField] protected int damage = 1;

    [SerializeField] Transform healthBar;

    [SerializeField] int pickupChance;
    [SerializeField] GameObject[] weaponPickup;

    public virtual void Start()
    {
        maxHealth = health;
        healthBar.localScale = new Vector3(1f, 1f);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health > 0)
        {
            healthBar.localScale = new Vector3(health / maxHealth, 1f);
        }
        else if (health <= 0)
        {
            healthBar.localScale = new Vector3(0f, 1f);
            int randomNumb = Random.Range(0, 101);
            if (randomNumb < pickupChance)
            {
                GameObject randomPickup = weaponPickup[Random.Range(0, weaponPickup.Length)];
                if (randomPickup.tag == "Heart")
                    Instantiate(randomPickup, gameObject.transform.position, transform.rotation * Quaternion.Euler(0, 0, 0));
                else
                    Instantiate(randomPickup, gameObject.transform.position, transform.rotation * Quaternion.Euler(0, 0, -84f));
            }
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