using UnityEngine;

public class Eraser : MonoBehaviour, IWeapon
{
    [Header("Projectile")]
    public GameObject projectile;
    public float projectileSpeed;
    public float projectileLifetime;
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

            GameObject projectile = Instantiate(this.projectile);
            projectile.transform.SetPositionAndRotation(transform.position, transform.rotation);
            projectile.transform.Translate(aimDirection, Space.World);
            gameObject.SetActive(false);
            projectile.GetComponent<Projectile>().speed = projectileSpeed;
            projectile.GetComponent<Projectile>().lifetime = projectileLifetime;
            Invoke("ResetRanged", attackCooldown - attackCooldown * 0.1f);
        }
    }

    public void M2(Vector2 aimDirection)
    {
        Debug.Log(name + " M2 ability does not exist");
    }

    private void ResetRanged()
    {
        gameObject.SetActive(true);
    }
}
