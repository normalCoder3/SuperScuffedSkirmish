using System.Collections.Generic;
using UnityEngine;

public class Sharpener : MonoBehaviour
{
    public float attackCooldown;
    private AudioManager audioManager;
    public ParticleSystem hitEffect;
    private int index;
    private float lastAttackTime;
    public GameObject projectile;
    public float projectileDamage;
    public float projectileLifetime;
    public float projectileSpeed;
    public List<Sprite> sprites;
    private GameObject target;

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
            projectile.GetComponent<IProjectile>().Configure(projectileDamage, projectileLifetime);
            projectile.GetComponent<SpriteRenderer>().flipY = GetComponent<SpriteRenderer>().flipY;
            projectile.GetComponent<Rigidbody2D>().velocity = projectileSpeed * aimDirection;
            projectile.transform.Translate(aimDirection, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            Instantiate(hitEffect, transform.position, transform.rotation, GameObject.Find("Enemies").transform);
            audioManager.PlaySound("hit2");
        }
    }
}
