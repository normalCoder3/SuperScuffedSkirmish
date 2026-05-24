using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    private GameObject crosshair;
    private GameObject healthIcon;
    private GameObject weapon;
    [Header("Health")]
    public TMP_Text healthText;
    public float health;
    private float maxHealth;
    [Header("Hits")]
    public ParticleSystem hitEffect;
    public float invincibilityTime;
    private float lastHitTime;


    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
        healthIcon = GameObject.Find("Health Icon");
        maxHealth = health;

        if (GameObject.FindGameObjectsWithTag("Ranged").Length == 1)
        {
            weapon = GameObject.FindGameObjectWithTag("Ranged");
        } 
        else if (GameObject.FindGameObjectsWithTag("Melee").Length == 1)
        {
            weapon = GameObject.FindGameObjectWithTag("Melee");
        }

        healthText.text = " " + health + "/" + health;
    }

    void Update()
    {
        Vector2 aimDirection = (crosshair.transform.position - weapon.transform.position).normalized;

        if (crosshair.GetComponent<SpriteRenderer>().sprite.name == "meleeCrosshair")
        {
            crosshair.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x));
        } 
        else if (crosshair.transform.rotation.z != 0)
        {
            crosshair.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        bool flip = (crosshair.transform.position - transform.position).x < 0;
        if (GetComponent<SpriteRenderer>().flipX != flip)
        {
            GetComponent<SpriteRenderer>().flipX = flip;
            weapon.GetComponent<SpriteRenderer>().flipY = GetComponent<SpriteRenderer>().flipX;
            weapon.transform.position = new Vector2(transform.position.x + (flip ? -0.25f : 0.25f), transform.position.y - 0.25f);
        }

        weapon.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x));

        if (Input.GetMouseButton(0))
        {
            weapon.GetComponent<IWeapon>().M1(aimDirection);
        }
        if (Input.GetMouseButton(1))
        {
            weapon.GetComponent<IWeapon>().M2(aimDirection);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "EnemyProjectile" || collision.tag == "Enemy" && Time.time - lastHitTime > invincibilityTime)
        {
            lastHitTime = Time.time;
            try
            {
                health -= collision.GetComponent<IProjectile>().GetDamage();
            }
            catch (NullReferenceException)
            {
                health -= collision.GetComponent<IEnemy>().GetDamage();
            }
            finally
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1);
                Invoke("ExitInvincibility", invincibilityTime);
                Instantiate(hitEffect, transform.position, transform.rotation);
            }

            healthText.text = " " + health + "/" + maxHealth;
            healthIcon.GetComponent<Image>().color = new Color((float)-Math.Pow(100, -health / maxHealth) + 1, (float)Math.Pow(25, health / maxHealth - 1), (float)Math.Pow(25, health / maxHealth - 1));

            if (health <= 0)
            {
                healthText.color = Color.black;
                gameObject.SetActive(false);
            }
        }
    }

    private void ExitInvincibility()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
