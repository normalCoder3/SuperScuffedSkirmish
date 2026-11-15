using UnityEngine;

public class Scribble : MonoBehaviour
{
    private AudioManager audioManager;
    public ParticleSystem hitEffect;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            Instantiate(hitEffect, transform.position, transform.rotation, GameObject.Find("Enemies").transform);
            audioManager.PlaySound("hit1");
        }
    }
}
