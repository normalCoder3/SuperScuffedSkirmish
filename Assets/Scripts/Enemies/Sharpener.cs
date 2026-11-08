using System.Collections.Generic;
using UnityEngine;

public class Sharpener : MonoBehaviour, IEnemy
{
    [Header("Setup")]
    private AudioManager audioManager;
    private GameController gameController;
    private int index;
    public List<Sprite> sprites;
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
    public int scoreValue;
    public float GetDamage() => damage;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        target = GetComponent<DefaultEnemyTracking>().target;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            health -= collision.GetComponent<IProjectile>().GetDamage();
            Instantiate(hitEffect, transform.position, transform.rotation, GameObject.Find("Enemies").transform);
            audioManager.PlaySound("hit2");

            if (health <= 0)
            {
                gameController.UpdateScore(scoreValue);
                audioManager.PlaySound("destroy");
                Destroy(gameObject);
            }
        }
    }
}
