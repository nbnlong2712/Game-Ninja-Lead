using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLV1 : Enemy
{
    [SerializeField] float stopDistance = 1f;
    [SerializeField] float timeAttack = 0;
    private Animator animator;
    private bool isDie;
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        isDie = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            if (!isDie && player != null)
            {
                if (Vector2.Distance(transform.position, player.transform.position) > stopDistance)
                {
                    animator.SetBool("isAttack", false);
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                }
                else
                {
                    if (Time.time > timeAttack)
                    {
                        Attack();
                        timeAttack = Time.time + timeBetweenAttack;
                    }
                }
            }
        }
    }

    public void Attack()
    {
        animator.SetBool("isAttack", true);
        if (player != null)
            player.TakeDamage(damage);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        isDie = true;
        animator.SetBool("isDeath", true);
    }
}