using UnityEngine;

public class Scribble : MonoBehaviour, IEnemy
{
    [Header("Movement")]
    private GameObject target;
    public float movementSpeed;
    [Header("Combat")]
    public float damage;
    public float health;
    public ParticleSystem hitEffect;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    
    void FixedUpdate()
    {
        bool flip = (target.transform.position - transform.position).x < 0;
        if (GetComponent<SpriteRenderer>().flipX != flip)
        {
            GetComponent<SpriteRenderer>().flipX = flip;
        }

        GetComponent<Rigidbody2D>().velocity = !target ? Vector2.zero : movementSpeed * Time.deltaTime * (target.transform.position - transform.position).normalized;
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

        if (health <= 0)
        {
            Instantiate(hitEffect, transform.position, transform.rotation, GameObject.Find("Enemies").transform);
            Destroy(gameObject);
        }

    }
}
