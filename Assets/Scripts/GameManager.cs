using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    private GameObject player;
    public float spawnCooldown;
    private float lastSpawnTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > spawnCooldown && player.activeInHierarchy)
        {
            lastSpawnTime = Time.time;
            
            GameObject enemy = Instantiate(this.enemy, GameObject.Find("Enemies").transform);
            enemy.transform.position = new Vector2(Random.Range(-6.5f, 11.5f), Random.Range(22.5f, 25.5f));
        }
    }
}
