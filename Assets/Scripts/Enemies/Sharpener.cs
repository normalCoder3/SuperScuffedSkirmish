using System.Collections.Generic;
using UnityEngine;

public class Sharpener : MonoBehaviour, IEnemy
{
    [Header("Configuration")]
    private int index;
    public List<Sprite> sprites;
    [Header("Movement")]
    private GameObject target;
    public float movementSpeed;
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
        target = GameObject.FindGameObjectWithTag("Player");

        index = Random.Range(0, 4);
        GetComponent<SpriteRenderer>().sprite = sprites[index];
    }

    void Update()
    {
        if (target != null && Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;
            Vector2 aimDirection = (target.transform.position - transform.position).normalized;
            GameObject projectile = Instantiate(this.projectile);
            projectile.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x)));
            projectile.transform.Translate(aimDirection, Space.World);
            projectile.GetComponent<IProjectile>().Configure(damage, projectileLifetime, projectileSpeed);
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

            GetComponent<Rigidbody2D>().velocity = movementSpeed * Time.deltaTime * (target.transform.position - transform.position).normalized;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }


    public float GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            health = Mathf.Max(health - collision.GetComponent<IProjectile>().GetDamage(), 0);
        }

        if (hitEffect && health <= 0)
        {
            Instantiate(hitEffect, transform.position, transform.rotation, GameObject.Find("Enemies").transform);
            hitEffect = null;
            Destroy(gameObject);
        }
    }
}
