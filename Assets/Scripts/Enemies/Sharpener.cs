using System.Collections.Generic;
using UnityEngine;

public class Sharpener : MonoBehaviour, IEnemy
{
    private AudioManager audioManager;
    [Header("Configuration")]
    private int index;
    public List<Sprite> sprites;
    [Header("Movement")]
    public float movementSpeed;
    private GameObject target;
    [Header("Combat")]
    public float attackCooldown;
    public float damage;
    public float health;
    public ParticleSystem hitEffect;
    private float lastAttackTime;
    public GameObject projectile;
    public float projectileLifetime;
    public float projectileSpeed;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        target = GameObject.FindGameObjectWithTag("Player");

        index = Random.Range(0, 4);
        GetComponent<SpriteRenderer>().sprite = sprites[index];

        lastAttackTime = Time.time - attackCooldown / 2;
    }

    void Update()
    {
        if (target != null && Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            Vector2 aimDirection = (target.transform.position - transform.position).normalized;
            GameObject projectile = Instantiate(this.projectile, transform.position, Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x)));
            projectile.GetComponent<IProjectile>().Configure(damage, projectileLifetime);
            projectile.GetComponent<SpriteRenderer>().flipY = GetComponent<SpriteRenderer>().flipY;
            projectile.GetComponent<Rigidbody2D>().velocity = projectileSpeed * aimDirection;
            projectile.transform.Translate(aimDirection, Space.World);
        }
    }

    void FixedUpdate()
    {
        if (target != null) {
            bool flip = (target.transform.position - transform.position).x < 0;
            if (GetComponent<SpriteRenderer>().flipX != flip)
            {
                GetComponent<SpriteRenderer>().flipX = flip;
            }

            GetComponent<Rigidbody2D>().velocity = movementSpeed * (target.transform.position - transform.position).normalized;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            health -= collision.GetComponent<IProjectile>().GetDamage();
            audioManager.PlaySound("hit2");

            if (hitEffect && health <= 0)
            {
                Instantiate(hitEffect, transform.position, transform.rotation, GameObject.Find("Enemies").transform);
                hitEffect = null;
                audioManager.PlaySound("destroy");
                Destroy(gameObject);
            }
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}
