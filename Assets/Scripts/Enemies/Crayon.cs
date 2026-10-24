using System.Collections.Generic;
using UnityEngine;

public class Crayon : MonoBehaviour, IEnemy
{
    private AudioManager audioManager;
    private GameController gameController;
    [Header("Configuration")]
    public List<Color> colors;
    private int index;
    public List<Sprite> sprites;
    [Header("Movement")]
    public float movementSpeed;
    private GameObject target;
    [Header("Combat")]
    public float damage;
    public float health;
    public ParticleSystem hitEffect;
    private GameObject line;
    public float lineUpdateInterval;
    public GameObject projectile;
    public int scoreValue;
    public float GetDamage() => damage;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        target = GameObject.FindGameObjectWithTag("Player");

        line = Instantiate(projectile, GameObject.Find("Enemies").transform);
        line.GetComponent<CrayonLine>().crayon = gameObject;
        line.GetComponent<IProjectile>().Configure(damage, 0, persistent: true);
        line.GetComponent<CrayonLine>().lineUpdateInterval = lineUpdateInterval;

        index = Random.Range(0, 4);
        GetComponent<SpriteRenderer>().sprite = sprites[index];
        line.GetComponent<LineRenderer>().startColor = colors[index];
        line.GetComponent<LineRenderer>().endColor = colors[index];
    }

    void FixedUpdate()
    {
        if (target != null)
        {
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
            ParticleSystem hitParticles = Instantiate(hitEffect, transform.position, transform.rotation, GameObject.Find("Enemies").transform);
            ParticleSystem.MainModule main = hitParticles.transform.Find("Particles").GetComponent<ParticleSystem>().main;
            main.startColor = colors[index];
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
