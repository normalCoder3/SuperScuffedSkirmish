using System.Collections.Generic;
using UnityEngine;

public class Crayon : MonoBehaviour
{
    private AudioManager audioManager;
    public List<Color> colors;
    public ParticleSystem hitEffect;
    private int index;
    private GameObject line;
    public float lineUpdateInterval;
    public GameObject projectile;
    public float projectileDamage;
    public List<Sprite> sprites;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        line = Instantiate(projectile, GameObject.Find("Enemies").transform);
        line.GetComponent<CrayonLine>().crayon = gameObject;
        line.GetComponent<IProjectile>().Configure(projectileDamage, 0, persistent: true);
        line.GetComponent<CrayonLine>().lineUpdateInterval = lineUpdateInterval;

        index = Random.Range(0, 4);
        GetComponent<SpriteRenderer>().sprite = sprites[index];
        line.GetComponent<LineRenderer>().startColor = colors[index];
        line.GetComponent<LineRenderer>().endColor = colors[index];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            ParticleSystem hitParticles = Instantiate(hitEffect, transform.position, transform.rotation, GameObject.Find("Enemies").transform);
            ParticleSystem.MainModule main = hitParticles.transform.Find("Particles").GetComponent<ParticleSystem>().main;
            main.startColor = colors[index];
            audioManager.PlaySound("hit2");
        }
    }
}
