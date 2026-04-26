using UnityEngine;

public class Eraser : MonoBehaviour, IWeapon
{
    [Header("Projectile")]
    public GameObject projectile;
    public int damage;
    public float lifetime;
    public int speed;
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
            projectile.GetComponent<Projectile>().damage = damage;
            projectile.GetComponent<Projectile>().lifetime = lifetime;
            projectile.GetComponent<Projectile>().speed = speed;
            Invoke("ResetThrowable", attackCooldown - attackCooldown * 0.1f);
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
