using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    public float damage;
    public bool flipY;
    public float lifetime;
    public float speed;
    public bool persistent;

    void Start()
    {
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
        if (!persistent && !tag.Contains(collision.tag))
        {
            Destroy(gameObject);
        }
    }

    public void Configure(float damage, float lifetime, float speed, bool persistent)
    {
        this.damage = damage;
        this.lifetime = lifetime;
        this.speed = speed;
        this.persistent = persistent;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void FlipY(bool flip)
    {
        GetComponent<SpriteRenderer>().flipY = flip;
    }

    public float GetDamage()
    {
        return damage;
    }
}
