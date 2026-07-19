using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    private GameObject crosshair;
    private GameObject healthIcon;
    private GameObject weapon;
    [Header("Health")]
    public float health;
    public Sprite healthIconImage;
    public Sprite hurtIconImage;
    public TMP_Text healthText;
    private float lastRegenTime;
    private float maxHealth;
    public float regenAmount;
    public float regenCooldown;
    public float regenDowntime;
    [Header("Hits")]
    public ParticleSystem hitEffect;
    public float invincibilityTime;
    private float lastHitTime;


    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
        healthIcon = GameObject.Find("Health Icon");
        maxHealth = health;

        if (GameObject.FindGameObjectsWithTag("RangedWeapon").Length == 1)
        {
            weapon = GameObject.FindGameObjectWithTag("RangedWeapon");
        } 
        else if (GameObject.FindGameObjectsWithTag("MeleeWeapon").Length == 1)
        {
            weapon = GameObject.FindGameObjectWithTag("MeleeWeapon");
        }

        UpdateHealth();
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
            weapon.transform.position = new(transform.position.x + (flip ? -0.25f : 0.25f), transform.position.y - 0.25f);
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

        if (health < maxHealth && Time.time - lastHitTime > regenDowntime)
        {
            if (Time.time - lastRegenTime > regenCooldown)
            {
                lastRegenTime = Time.time;
                health = Mathf.Min(health + regenAmount, maxHealth);
                healthIcon.GetComponent<Image>().sprite = healthIconImage;
                UpdateHealth();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.CompareTag("EnemyProjectile") || collision.CompareTag("Enemy")) && Time.time - lastHitTime > invincibilityTime)
        {
            lastHitTime = Time.time;
            try
            {
                health = Mathf.Max(health - collision.GetComponent<IProjectile>().GetDamage(), 0);
            }
            catch (NullReferenceException)
            {
                health = Mathf.Max(health - collision.GetComponent<IEnemy>().GetDamage(), 0);
            }
            finally
            {
                GetComponent<SpriteRenderer>().color = new(1, 0.5f, 0.5f, 1);
                healthIcon.GetComponent<Image>().sprite = hurtIconImage;
                Invoke(nameof(ExitInvincibility), invincibilityTime);
                Instantiate(hitEffect, transform.position, transform.rotation);
                UpdateHealth();
            }

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

    private void UpdateHealth()
    {
        healthText.text = " " + health + "/" + maxHealth;
        healthIcon.GetComponent<Image>().color = new((float)-Math.Pow(100, -health / maxHealth) + 1, (float)Math.Pow(25, health / maxHealth - 1), (float)Math.Pow(25, health / maxHealth - 1));
    }
}
