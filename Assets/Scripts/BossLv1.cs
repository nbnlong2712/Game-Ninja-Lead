using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLv1 : Enemy
{
    [SerializeField] float stopDistance = 5f;
    private float attackTime = 0f;
    Player player;
    Animator animator;
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject enemyChild;
    [HideInInspector] public bool isDie;

    public override void Start()
    {
        base.Start();
        isDie = false;
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            if (Time.time >= attackTime)
            {
                attackTime = Time.time + timeBetweenAttack;
                animator.SetTrigger("attack");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (player != null)
            {
                player.TakeDamage(damage);
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
            if (player.transform.position.x > transform.position.x)
            {
                bullet.transform.localScale = new Vector3(-1, bullet.transform.localScale.y, bullet.transform.localScale.z);
            }
            else
            {
                bullet.transform.localScale = new Vector3(1, bullet.transform.localScale.y, bullet.transform.localScale.z);
            }
            Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        }
    }

    public void Summon()
    {
        if (player != null)
        {
            Instantiate(enemyChild, transform.position, transform.rotation);
        }
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
