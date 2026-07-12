using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crayon : MonoBehaviour, IEnemy
{
    /*
    Create projectile prefab for crayon line "projectile"
    Copy and paste relevant components
    Create crayon line script, assign source crayon from this script first
    Maybe update some packages too
    */
    private GameObject line;
    [Header("Configuration")]
    public List<Color> colors;
    private int index;
    public List<Sprite> sprites;
    [Header("Movement")]
    private GameObject target;
    public float movementSpeed;
    [Header("Combat")]
    public float damage;
    public float health;
    public ParticleSystem hitEffect;
    private float lastLineUpdate;
    private Vector2[] linePoints;
    public float lineUpdateInterval;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        index = Random.Range(0, 4);
        GetComponent<SpriteRenderer>().sprite = sprites[index];
        GetComponent<LineRenderer>().startColor = colors[index];
        GetComponent<LineRenderer>().endColor = colors[index];

        linePoints = new Vector2[GetComponent<LineRenderer>().positionCount];
        for (int i = 0; i < GetComponent<LineRenderer>().positionCount; i++)
        {
            GetComponent<LineRenderer>().SetPosition(i, new(transform.position.x, transform.position.y - 0.95f));
            linePoints[i] = transform.InverseTransformPoint(GetComponent<LineRenderer>().GetPosition(i));
        }
        GetComponent<EdgeCollider2D>().SetPoints(linePoints.ToList());
    }

    void FixedUpdate()
    {
        bool flip = (target.transform.position - transform.position).x < 0;
        if (GetComponent<SpriteRenderer>().flipX != flip)
        {
            GetComponent<SpriteRenderer>().flipX = flip;
        }

        GetComponent<Rigidbody2D>().velocity = !target ? Vector2.zero : movementSpeed * Time.deltaTime * (target.transform.position - transform.position).normalized;
    }

    void Update()
    {
        if (Time.time - lastLineUpdate > lineUpdateInterval)
        {
            lastLineUpdate = Time.time;

            for (int i = 0; i < GetComponent<LineRenderer>().positionCount; i++)
            {
                if (i == GetComponent<LineRenderer>().positionCount - 1)
                {
                    GetComponent<LineRenderer>().SetPosition(i, new(transform.position.x, transform.position.y - 0.95f));
                }
                else
                {
                    GetComponent<LineRenderer>().SetPosition(i, GetComponent<LineRenderer>().GetPosition(i + 1));
                }
                linePoints[i] = transform.InverseTransformPoint(GetComponent<LineRenderer>().GetPosition(i));
            }
            GetComponent<EdgeCollider2D>().SetPoints(linePoints.ToList());
        }
    }

    public float GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            health = Mathf.Max(health - collision.GetComponent<IProjectile>().GetDamage(), 0);
        }

        if (hitEffect && health <= 0)
        {
            ParticleSystem hitParticles = Instantiate(hitEffect, transform.position, transform.rotation, GameObject.Find("Enemies").transform);
            ParticleSystem.MainModule main = hitParticles.transform.Find("Particles").GetComponent<ParticleSystem>().main;
            main.startColor = colors[index];
            hitEffect = null;
            Destroy(gameObject);
        }
    }
}
