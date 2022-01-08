using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    [Header("Dragon bullet")]
    [SerializeField] Image dragonImage;
    [SerializeField] float coolDown1 = 30;
    bool isCoolDown1 = false;

    [Header("Shield")]
    [SerializeField] Image shieldImage;
    [SerializeField] float coolDown2 = 20;
    bool isCoolDown2 = false;

    [Header("Healing")]
    [SerializeField] Image healImage;
    [SerializeField] float coolDown3 = 60;
    bool isCoolDown3 = false;

    Player player;
    Shield shield;
    [SerializeField] Bullet dragonBullet;

    void Start()
    {
        shield = FindObjectOfType<Shield>();
        if(shield != null)
        {
            shield.gameObject.SetActive(false);
        }
        player = FindObjectOfType<Player>();
        dragonImage.fillAmount = 0;
        shieldImage.fillAmount = 0;
        healImage.fillAmount = 0;
    }

    void Update()
    {
        DragonFire();
        Shield();
        Heal();
    }

    public void DragonFire()
    {
        if (Input.GetKey(KeyCode.U) && !isCoolDown1)
        {
            isCoolDown1 = true;
            dragonImage.fillAmount = 1;
            Weapon weapon = FindObjectOfType<Weapon>();
            if(weapon != null)
            {
                if(player!= null)
                {
                    if(player.transform.position.x < weapon.getShotPoint().x)
                    {
                        dragonBullet.transform.localScale = new Vector3(1, dragonBullet.transform.localScale.y, dragonBullet.transform.localScale.z);
                    }
                    else
                    {
                        dragonBullet.transform.localScale = new Vector3(-1, dragonBullet.transform.localScale.y, dragonBullet.transform.localScale.z);
                    }
                }
                Instantiate(dragonBullet, weapon.getShotPoint(), weapon.getRotation());
            }
        }
        if (isCoolDown1)
        {
            dragonImage.fillAmount -= 1 / coolDown1 * Time.deltaTime;
            if (dragonImage.fillAmount <= 0)
            {
                dragonImage.fillAmount = 0;
                isCoolDown1 = false;
            }
        }
    }

    public void Shield()
    {
        if (Input.GetKey(KeyCode.I) && !isCoolDown2)
        {
            isCoolDown2 = true;
            shieldImage.fillAmount = 1;

            if(shield != null)
            {
                StartCoroutine(getShield());
            }
        }
        if (isCoolDown2)
        {
            shieldImage.fillAmount -= 1 / coolDown2 * Time.deltaTime;
            if (shieldImage.fillAmount <= 0)
            {
                shieldImage.fillAmount = 0;
                isCoolDown2 = false;
            }
        }
    }

    IEnumerator getShield()
    {
        shield.gameObject.SetActive(true);
        yield return new WaitForSeconds(7);
        shield.gameObject.SetActive(false);
    }

    public void Heal()
    {
        if (Input.GetKey(KeyCode.O) && !isCoolDown3)
        {
            if(player != null)
            {
                player.IncreaseHeart(2);
                player.PlayHealExplosion();
            }
            isCoolDown3 = true;
            healImage.fillAmount = 1;
        }
        if (isCoolDown3)
        {
            healImage.fillAmount -= 1 / coolDown3 * Time.deltaTime;
            if (healImage.fillAmount <= 0)
            {
                healImage.fillAmount = 0;
                isCoolDown3 = false;
            }
        }
    }
}
