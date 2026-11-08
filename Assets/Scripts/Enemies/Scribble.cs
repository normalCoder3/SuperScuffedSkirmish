using UnityEngine;

public class Scribble : MonoBehaviour, IEnemy
{
    [Header("Setup")]
    private AudioManager audioManager;
    private GameController gameController;
    [Header("Combat")]
    public float damage;
    public float health;
    public ParticleSystem hitEffect;
    public int scoreValue;
    public float GetDamage() => damage;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile"))
        {
            health -= collision.GetComponent<IProjectile>().GetDamage();
            Instantiate(hitEffect, transform.position, transform.rotation, GameObject.Find("Enemies").transform);
            audioManager.PlaySound("hit1");

            if (health <= 0)
            {
                gameController.UpdateScore(scoreValue);
                audioManager.PlaySound("destroy");
                Destroy(gameObject);
            }
        }
    }
}
