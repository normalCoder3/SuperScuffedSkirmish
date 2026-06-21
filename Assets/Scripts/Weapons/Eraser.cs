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

            GameObject projectile = Instantiate(this.projectile);
            projectile.transform.SetPositionAndRotation(transform.position, transform.rotation);
            projectile.transform.Translate(aimDirection, Space.World);
            projectile.GetComponent<IProjectile>().Configure(damage, lifetime, speed);
            gameObject.SetActive(false);
            Invoke(nameof(ResetThrowable), attackCooldown * 0.9f);
        }
    }

    public void M2(Vector2 aimDirection)
    {
        Debug.Log(name + " M2 ability does not exist");
    }

    private void ResetThrowable()
    {
        gameObject.SetActive(true);
    }
}
