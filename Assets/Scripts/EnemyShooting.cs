using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Enemy")]
    private GameObject target;
    public GameObject gun;
    [Header("Firearm")]
    public GameObject projectile;
    public int projectileSpeed;
    public int projectileLifetime;
    public float firingCooldown;
    private float lastFiringTime;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 aimDirection = (target.transform.position - gun.transform.position).normalized;
        bool flip = (target.transform.position - transform.position).x < 0;
        if (GetComponent<SpriteRenderer>().flipX != flip)
        {
            GetComponent<SpriteRenderer>().flipX = flip;
            gun.GetComponent<SpriteRenderer>().flipY = flip;
            gun.transform.position = new Vector2(transform.position.x + (flip ? -0.25f : 0.25f), transform.position.y - 0.25f);
        }
        gun.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x));

        if (Time.time - lastFiringTime > firingCooldown)
        {
            lastFiringTime = Time.time;
            GameObject projectile = Instantiate(this.projectile);
            projectile.transform.SetPositionAndRotation(gun.transform.position, gun.transform.rotation);
            projectile.transform.Translate(new Vector2(aimDirection.x, aimDirection.y), Space.World);
            projectile.GetComponent<Projectile>().speed = projectileSpeed;
            projectile.GetComponent<Projectile>().lifetime = projectileLifetime;
        }
    }
}
