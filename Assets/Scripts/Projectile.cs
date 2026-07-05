using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{

    public float damage;
    public float lifetime;
    public float speed;
    public bool flipY;

    void Start()
    {
        GetComponent<SpriteRenderer>().flipY = flipY;
        if (lifetime > 0)
        {
            Invoke(nameof(Destroy), lifetime);
        }
    }

    void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!tag.Contains(collision.tag))
        {
            Destroy(gameObject);
        }
    }

    public void Configure(float damage, float lifetime, float speed)
    {
        this.damage = damage;
        this.lifetime = lifetime;
        this.speed = speed;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}
