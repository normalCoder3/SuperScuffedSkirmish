using UnityEngine;

public class Eraser : MonoBehaviour, IWeapon
{
    [Header("Projectile")]
    public GameObject projectile;
    public float damage;
    public float lifetime;
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
            projectile.GetComponent<IProjectile>().Configure(damage, lifetime, speed);
            projectile.transform.Translate(aimDirection, Space.World);
            projectile.GetComponent<IProjectile>().FlipY(GetComponent<SpriteRenderer>().flipY);
            gameObject.SetActive(false);
            Invoke(nameof(ResetThrowable), attackCooldown * 0.9f);
        }
    }

    public void M2(Vector2 aimDirection)
    {
        Debug.Log(name + " has no M2 ability!");
    }

    private void ResetThrowable()
    {
        gameObject.SetActive(true);
    }
}
