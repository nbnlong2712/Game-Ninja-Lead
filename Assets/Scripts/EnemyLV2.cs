using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLV2 : Enemy
{
    [SerializeField] float minX;
    [SerializeField] float minY;
    [SerializeField] float maxX;
    [SerializeField] float maxY;

    [SerializeField] Enemy enemyLv1;

    Animator anim;
    Vector2 targetPosition;
    [SerializeField] float timeBetweenSummon = 6f;
    float summonTime;
    float timeToMove = 0;
    [SerializeField] float timeBetweenMove = 10f;
    Player player;

    public override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();    
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        MoveEnemyLv2();
    }

    void MoveEnemyLv2()
    {
        float randomX = Random.Range(minX + 5, maxX - 5);
        float randomY = Random.Range(minY + 5, maxY - 5);

        if (Time.time == 0)
            targetPosition = new Vector2(randomX, randomY);
        else if (Time.time > timeToMove)
        {
            timeToMove = Time.time + timeBetweenMove;
            targetPosition = new Vector2(randomX, randomY);
        }
        if (player != null)
        {
            if (Vector2.Distance(transform.position, targetPosition) > 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
                if (Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBetweenSummon;
                    anim.SetTrigger("summon");
                }
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        anim.SetBool("isDeath", true);
    }

    public void Summon()
    {
        if (player != null)
        {
            Instantiate(enemyLv1, transform.position, transform.rotation);
        }
    }
}