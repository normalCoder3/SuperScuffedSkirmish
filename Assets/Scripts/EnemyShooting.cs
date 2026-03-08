using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Enemy")]
    private GameObject target;
    public GameObject weapon;
    [Header("Weapon")]
    public GameObject projectile;
    public int projectileSpeed;
    public int projectileLifetime;
    public float attackCooldown;
    private float lastAttackTime;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 aimDirection = (target.transform.position - weapon.transform.position).normalized;
        bool flip = (target.transform.position - transform.position).x < 0;

        if (GetComponent<SpriteRenderer>().flipX != flip)
        {
            GetComponent<SpriteRenderer>().flipX = flip;
            weapon.GetComponent<SpriteRenderer>().flipY = flip;
            weapon.transform.position = new Vector2(transform.position.x + (flip ? -0.25f : 0.25f), transform.position.y - 0.25f);
        }

        weapon.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x));

        if (Time.time - lastAttackTime > attackCooldown)
        {
            if (weapon.tag == "Ranged")
            {
                lastAttackTime = Time.time;

                GameObject projectile = Instantiate(this.projectile);
                Projectile projectileScript = projectile.GetComponent<Projectile>();
                projectile.transform.SetPositionAndRotation(weapon.transform.position, weapon.transform.rotation);
                projectile.transform.Translate(new Vector2(aimDirection.x, aimDirection.y), Space.World);
                projectileScript.speed = projectileSpeed;
                projectileScript.lifetime = projectileLifetime;
                projectileScript.flipY = GetComponent<SpriteRenderer>().flipX;
            }
        }
    }
}
