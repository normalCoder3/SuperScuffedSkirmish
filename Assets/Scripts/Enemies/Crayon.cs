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
    public float lineUpdateInterval;

    void Start()
    {
        // NEXT TIME CREATE A LINE PREFAB AND CONFIGURE IT BEFOREHAND
        target = GameObject.FindGameObjectWithTag("Player");
        line = new GameObject("Crayon Line", typeof(LineRenderer));
        line.transform.SetParent(GameObject.Find("Enemy").transform);
        line.GetComponent<LineRenderer>().positionCount = 1;
        line.GetComponent<LineRenderer>().startColor = lineColor;
        line.GetComponent<LineRenderer>().endColor = lineColor;
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
            for (int i = 0; i < 5; i++)
            {
                //if (i == 4)
                //{
                //    line.GetComponent<LineRenderer>().SetPosition(i, new Vector2(transform.position.x, transform.position.y - 1));
                //}
                //else
                //{
                //    line.GetComponent<LineRenderer>().SetPosition(i, line.GetComponent<LineRenderer>().GetPosition(i + 1));
                //}
            }
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
            Destroy(gameObject);
        }

    }
}
