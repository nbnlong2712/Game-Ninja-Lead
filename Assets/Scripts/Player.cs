using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float speed = 50f;
    new Rigidbody2D rigidbody2D;
    Animator animator;
    [SerializeField] float health = 150f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
    }

    void OnMove(InputValue inputValue)
    {
        if (inputValue != null)
        {
            moveInput = inputValue.Get<Vector2>();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        speed = 0;
        animator.SetBool("isDeath", true);
        GameObject.FindGameObjectWithTag("Weapon").SetActive(false);
        Invoke("DestroyPlayer", 2);
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }

    void Run()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + moveInput.normalized * speed * Time.deltaTime);
        if (moveInput != Vector2.zero)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void ChangeWeapon(Weapon weapon)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weapon, transform.position - new Vector3(1.34f, 0, 0), transform.rotation, transform);
    }
}