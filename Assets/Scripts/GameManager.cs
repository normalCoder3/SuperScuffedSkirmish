using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject indicator;
    public GameObject[] enemy;
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
            
            GameObject indicator = Instantiate(this.indicator, GameObject.Find("Enemies").transform);
            indicator.transform.position = new(Random.Range(-6.5f, 11.5f), Random.Range(22f, 25f));
            indicator.GetComponent<Indicator>().enemy = enemy[Random.Range(0, enemy.Length)];
        }
    }
}
