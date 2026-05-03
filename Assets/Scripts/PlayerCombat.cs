using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private GameObject crosshair;
    private GameObject weapon;
    public float health;
    public float invincibilityTime;
    private float lastHitTime;
    public ParticleSystem hitEffect;


    void Start()
    {
        crosshair = GameObject.Find("Crosshair");

        if (GameObject.FindGameObjectsWithTag("Melee").Length == 1)
        {
            weapon = GameObject.FindGameObjectWithTag("Melee");
        } 
        else if (GameObject.FindGameObjectsWithTag("Ranged").Length == 1)
        {
            weapon = GameObject.FindGameObjectWithTag("Ranged");
        }
    }

    void Update()
    {
        Vector2 aimDirection = (crosshair.transform.position - weapon.transform.position).normalized;

        if (crosshair.GetComponent<SpriteRenderer>().sprite.name == "meleeCrosshair")
        {
            crosshair.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x));
        } 
        else if (crosshair.transform.rotation.z != 0)
        {
            crosshair.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        bool flip = (crosshair.transform.position - transform.position).x < 0;
        if (GetComponent<SpriteRenderer>().flipX != flip)
        {
            GetComponent<SpriteRenderer>().flipX = flip;
            weapon.GetComponent<SpriteRenderer>().flipY = GetComponent<SpriteRenderer>().flipX;
            weapon.transform.position = new Vector2(transform.position.x + (flip ? -0.25f : 0.25f), transform.position.y - 0.25f);
        }

        weapon.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(aimDirection.y, aimDirection.x));

        if (Input.GetMouseButton(0))
        {
            weapon.GetComponent<IWeapon>().M1(aimDirection);
        }
        if (Input.GetMouseButton(1))
        {
            weapon.GetComponent<IWeapon>().M2(aimDirection);
        }
    }

    // Doesn't work with OnTriggerStay2D, for some reason
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.time - lastHitTime > invincibilityTime)
        {
            lastHitTime = Time.time;
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 123);
            Invoke("ResetColor", invincibilityTime);

            if (collision.tag == "EnemyProjectile")
            {
                Instantiate(hitEffect, transform.position, transform.rotation);
                health -= collision.GetComponent<IProjectile>().GetDamage();
                Debug.Log(health + " HP left after being hit by a " + collision.name);
            }
            else if (collision.tag == "Enemy")
            {
                Instantiate(hitEffect, transform.position, transform.rotation);
                health -= collision.GetComponent<IEnemy>().GetDamage();
                Debug.Log(health + " HP left after being hit by a " + collision.name);
            }

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ResetColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
