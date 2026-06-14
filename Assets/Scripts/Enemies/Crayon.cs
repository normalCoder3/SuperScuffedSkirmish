using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crayon : MonoBehaviour, IEnemy
{
    [Header("Movement")]
    private GameObject target;
    public float movementSpeed;
    [Header("Combat")]
    public float damage;
    public float health;
    //public ParticleSystem hitEffect;
    private GameObject line;
    public Color lineColor;
    private float lastLineUpdate;
    private Vector2[] linePoints = new Vector2[50];
    public float lineUpdateInterval;
    public GameObject projectile;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        line = Instantiate(projectile, GameObject.Find("Enemies").transform);
        line.GetComponent<LineRenderer>().startColor = lineColor;
        line.GetComponent<LineRenderer>().endColor = lineColor;
        for (int i = 0; i < line.GetComponent<LineRenderer>().positionCount; i++)
        {
            Vector2 crayonTip = new Vector2(transform.position.x, transform.position.y - 0.95f);
            line.GetComponent<LineRenderer>().SetPosition(i, crayonTip);
            linePoints[i] = crayonTip;
        }
        line.GetComponent<EdgeCollider2D>().SetPoints(linePoints.ToList());
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
        line.GetComponent<LineRenderer>().SetPosition(line.GetComponent<LineRenderer>().positionCount - 1, new Vector2(transform.position.x, transform.position.y - 0.95f));

        if (Time.time - lastLineUpdate > lineUpdateInterval)
        {
            lastLineUpdate = Time.time;

            for (int i = 0; i < line.GetComponent<LineRenderer>().positionCount; i++)
            {
                Vector2 crayonTip = new Vector2(transform.position.x, transform.position.y - 0.95f);

                if (i == line.GetComponent<LineRenderer>().positionCount - 1)
                {
                    line.GetComponent<LineRenderer>().SetPosition(i, crayonTip);
                }
                else
                {
                    line.GetComponent<LineRenderer>().SetPosition(i, line.GetComponent<LineRenderer>().GetPosition(i + 1));
                }
                linePoints[i] = crayonTip;
            }
            line.GetComponent<EdgeCollider2D>().SetPoints(linePoints.ToList());
        }
    }

    public float GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {
            health = Mathf.Max(health - collision.GetComponent<IProjectile>().GetDamage(), 0);
        }

        if (health <= 0)
        {
            //Instantiate(hitEffect, transform.position, transform.rotation, GameObject.Find("Enemies").transform);
            Destroy(line);
            Destroy(gameObject);
        }

    }
}
