using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] AudioClip pickupSound;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlaySound(pickupSound);
            collision.GetComponent<Player>().ChangeWeapon(weapon);
            Destroy(gameObject);
        }
    }
    public void PlaySound(AudioClip audio)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audio);
        }
    }
}
