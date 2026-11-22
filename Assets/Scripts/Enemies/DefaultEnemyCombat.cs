using UnityEngine;

public class DefaultEnemyCombat : MonoBehaviour, IEnemy
{
    private AudioManager audioManager;
    public float damage;
    private GameController gameController;
    public float health;
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

            if (health <= 0)
            {
                gameController.UpdateScore(scoreValue);
                gameController.UpdateEnemyCount(1);
                audioManager.PlaySound("destroy");
                Destroy(gameObject);
            }
        }
    }
}
