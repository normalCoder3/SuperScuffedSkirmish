using UnityEngine;

public class Scribble : MonoBehaviour, IEnemy
{
    private AudioManager audioManager;
    [Header("Movement")]
    public float movementSpeed;
    private GameObject target;
    [Header("Combat")]
    public float damage;
    public float health;
    public ParticleSystem hitEffect;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        target = GameObject.FindGameObjectWithTag("Player");
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
            collision.GetComponent<Collider2D>().enabled = false;
            health -= collision.GetComponent<IProjectile>().GetDamage();
            audioManager.PlaySound("hit1");

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
