using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    private GameObject crosshair;
    private GameObject healthIcon;
    private GameObject weapon;
    public float health;
    private float maxHealth;
    public float invincibilityTime;
    private float lastHitTime;
    public ParticleSystem hitEffect;


    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
        maxHealth = health;
        healthIcon = GameObject.Find("Health Icon");

        if (GameObject.FindGameObjectsWithTag("Ranged").Length == 1)
        {
            weapon = GameObject.FindGameObjectWithTag("Ranged");
        } 
        else if (GameObject.FindGameObjectsWithTag("Melee").Length == 1)
        {
            weapon = GameObject.FindGameObjectWithTag("Melee");
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "EnemyProjectile" || collision.tag == "Enemy" && Time.time - lastHitTime > invincibilityTime)
        {
            lastHitTime = Time.time;
            try
            {
                health -= collision.GetComponent<IProjectile>().GetDamage();
            }
            catch (NullReferenceException)
            {
                health -= collision.GetComponent<IEnemy>().GetDamage();
            }
            finally
            {
                Color color = GetComponent<SpriteRenderer>().color;
                GetComponent<SpriteRenderer>().color = new Color(1, color.g / 2, color.b / 2, 1);
                Invoke("ExitInvincibility", invincibilityTime);
                Instantiate(hitEffect, transform.position, transform.rotation);
                Debug.Log(health + " HP left after being hit by a " + collision.name);
            }

            // Red doesn't work
            float healthDifference = 1 - health / maxHealth;
            healthIcon.GetComponent<Image>().color = new Color( (float) (Math.Pow(-100, healthDifference - 1) + 1), (float) Math.Pow(100, -healthDifference), (float) Math.Pow(100, -healthDifference), 1);
            Debug.Log(healthIcon.GetComponent<Image>().color);

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ExitInvincibility()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GetComponent<SpriteRenderer>().color.a);
    }
}
