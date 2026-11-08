using UnityEngine;

public class Eraser : MonoBehaviour, IWeapon
{
    [Header("Projectile")]
    public float damage;
    public float lifetime;
    public GameObject projectile;
    public float speed;
    [Header("Combat")]
    public float attackCooldown;
    private float lastAttackTime;


    void Start()
    {
        GameObject.Find("Crosshair").GetComponent<Crosshair>().SetRanged();
    }

    public void M1(Vector2 aimDirection)
    {
        if (Time.time - lastAttackTime > attackCooldown)
        {
            lastAttackTime = Time.time;

            GameObject projectile = Instantiate(this.projectile, transform.position, transform.rotation);
            projectile.transform.Translate(aimDirection, Space.World);
            projectile.GetComponent<IProjectile>().Configure(damage, lifetime);
            projectile.GetComponent<SpriteRenderer>().flipY = GetComponent<SpriteRenderer>().flipY;
            projectile.GetComponent<Rigidbody2D>().velocity = speed * aimDirection;
            gameObject.SetActive(false);
            Invoke(nameof(ResetThrowable), attackCooldown * 0.9f);
        }
    }

    public void M2(Vector2 aimDirection) { }

    private void ResetThrowable()
    {
        gameObject.SetActive(true);
    }
}
