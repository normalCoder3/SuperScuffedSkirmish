using UnityEngine;

public class Scribble : MonoBehaviour, IEnemy
{
    private AudioManager audioManager;
    private GameController gameController;
    [Header("Movement")]
    public float movementSpeed;
    private GameObject target;
    [Header("Combat")]
    public float damage;
    public float health;
    public ParticleSystem hitEffect;
    public int scoreValue;
    public float GetDamage() => damage;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            bool flip = (target.transform.position - transform.position).x < 0;
            if (GetComponent<SpriteRenderer>().flipX != flip) GetComponent<SpriteRenderer>().flipX = flip;
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
            Instantiate(hitEffect, transform.position, transform.rotation, GameObject.Find("Enemies").transform);
            audioManager.PlaySound("hit1");

            if (health <= 0)
            {
                gameController.UpdateScore(scoreValue);
                audioManager.PlaySound("destroy");
                Destroy(gameObject);
            }
        }
    }
}
