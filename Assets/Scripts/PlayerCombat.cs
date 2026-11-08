using System;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [Header("Setup")]
    private AudioManager audioManager;
    private GameObject crosshair;
    private GameController gameController;
    private GameObject healthIcon;
    private SpriteRenderer spriteRenderer;
    private GameObject weapon;
    [Header("Health")]
    public float health;
    public Sprite healthIconImage;
    public TMP_Text healthText;
    public Sprite hurtIconImage;
    private float lastRegenTime;
    private float maxHealth;
    public float regenAmount;
    public float regenCooldown;
    public float regenDowntime;
    [Header("Hits")]
    public ParticleSystem hitEffect;
    public float invincibilityTime;
    private float lastHitTime;
    public int hitScoreDeduction;
    [Header("Weapons")]
    public GameObject[] weapons;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        crosshair = GameObject.Find("Crosshair");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        healthIcon = GameObject.Find("Health Icon");
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHealth = health;

        SetWeapon(GameData.weaponIndex);
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

        bool flip = (crosshair.transform.position - transform.position).x < 0;
        if (spriteRenderer.flipX != flip)
        {
            spriteRenderer.flipX = flip;
            weapon.GetComponent<SpriteRenderer>().flipY = spriteRenderer.flipX;
            weapon.transform.position = new(transform.position.x + (flip ? -0.25f : 0.25f), transform.position.y - 0.25f);
        }

        weapon.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x));

        if (Input.GetMouseButton(0)) weapon.GetComponent<IWeapon>().M1(aimDirection);
        if (Input.GetMouseButton(1)) weapon.GetComponent<IWeapon>().M2(aimDirection);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile")) {
            TakeHit(collision.GetComponent<IProjectile>().GetDamage());
        }
        else if (collision.CompareTag("Enemy"))
        {
            TakeHit(collision.GetComponent<IEnemy>().GetDamage());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            TakeHit(collision.GetComponent<IProjectile>().GetDamage());
        }
        else if (collision.CompareTag("Enemy"))
        {
            TakeHit(collision.GetComponent<IEnemy>().GetDamage());
        }
    }

    private void ExitInvincibility()
    {
        spriteRenderer.color = Color.white;
    }

    private void SetWeapon(int index)
    {
        if (weapon != null) Destroy(weapon);
        weapon = Instantiate(weapons[index], transform);
    }

    private void TakeHit(float damage)
    {
        if (Time.time - lastHitTime > invincibilityTime)
        {
            lastHitTime = Time.time;
            health -= damage;
            spriteRenderer.color = new(1, 0.5f, 0.5f, 1);
            healthIcon.GetComponent<Image>().sprite = hurtIconImage;
            Invoke(nameof(ExitInvincibility), invincibilityTime);
            Instantiate(hitEffect, transform.position, transform.rotation);
            gameController.UpdateScore(hitScoreDeduction);
            audioManager.PlaySound("hit1");
            UpdateHealth();

            if (health <= 0)
            {
                healthText.color = Color.black;
                audioManager.PlaySound("destroy");
                Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.SetActive(false);
                Camera.main.GetComponent<AudioSource>().Stop();
                Destroy(gameObject);
            }
        }
    }

    private void UpdateHealth()
    {
        healthText.text = " " + health + "/" + maxHealth;
        healthIcon.GetComponent<Image>().color = new((float)-Math.Pow(100, -health / maxHealth) + 1, (float)Math.Pow(25, health / maxHealth - 1), (float)Math.Pow(25, health / maxHealth - 1));
    }
}
