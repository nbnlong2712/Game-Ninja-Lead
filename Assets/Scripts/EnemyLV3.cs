using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLV3 : Enemy
{
    [SerializeField] float stopDistance = 5f;
    private float attackTime;
    Player player;
    Animator animator;
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject bullet;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if(player != null)
        {
            if(Vector2.Distance(transform.position, player.transform.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else
            {
                if (Time.time > attackTime)
                {
                    attackTime += Time.time + timeBetweenAttack;
                    animator.SetTrigger("attack");
                }
            }
        }
    }

    public void Attack()
    {
        if (player != null)
        {
            Vector2 direction = player.transform.position - shotPoint.position;    //direction: hướng bắn: muốn tính hướng bắn, lấy hai vector trừ nhau (toán lớp 10)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            shotPoint.rotation = rotation;
            Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        animator.SetBool("isDeath", true);
    }
}
