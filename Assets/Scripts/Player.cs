using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Vector2 moveInput;
    [SerializeField] float speed = 50f;
    new Rigidbody2D rigidbody2D;
    Animator animator;
    [SerializeField] int health = 8;

    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite lostHeart;
    [SerializeField] Animator hurtPanel;
    CameraShake cameraShake;
    AudioSource audioSource;
    [SerializeField] AudioClip damageSound;
    LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
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

    public void TakeDamage(int damage)
    {

        if (health > 0)
        {
            PlaySound(damageSound);
            CameraShake.Instance.ShakeCamera(8f, 0.2f);
            if (hurtPanel != null)
                hurtPanel.SetTrigger("hurt");
            health -= damage;
            TakeHeart(health);
        }
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeHeart(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = lostHeart;
            }
        }
    }

    public void Die()
    {
        speed = 0;
        animator.SetBool("isDeath", true);
        destroyObjectWithTag("Weapon");
        Invoke("DestroyPlayer", 2);
        if (levelManager != null)
        {
            levelManager.LoadGameOver();
        }
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }

    public void destroyObjectWithTag(string tag)
    {
        foreach (Transform child in transform)
        {
            if (child.tag == tag)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void IncreaseHeart(int heart)
    {
        if (health + heart > 10)
            health = 10;
        else health += heart;
        TakeHeart(health);
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
        destroyObjectWithTag("Weapon");
        Instantiate(weapon, transform.position - new Vector3(1.34f, 0, 0), transform.rotation, transform);
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}