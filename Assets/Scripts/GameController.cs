using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject indicator;
    private float lastSpawnTime;
    private float lastScoreTime;
    private GameObject player;
    public int score;
    public int scoreInterval;
    public TMP_Text scoreText;
    public float spawnCooldown;
    private float startTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startTime = Time.time;
    }

    void Update()
    {
        if (player == null)
        {
            Invoke(nameof(Lose), 1);
        } 
        else
        {
            if (Time.time - lastScoreTime > scoreInterval)
            {
                lastScoreTime = Time.time;
                UpdateScore(100);
            }

            if ((Time.time - lastSpawnTime > spawnCooldown * Mathf.Pow(0.9999f, Mathf.Pow(Time.time - startTime, 2))) && player.activeInHierarchy)
            {
                lastSpawnTime = Time.time;

                GameObject indicator = Instantiate(this.indicator, GameObject.Find("Enemies").transform);
                indicator.transform.position = new(Random.Range(-6.5f, 11.5f), 24f);

                int index = 0;
                for (int i = 0; i < enemy.Length - 1; i++)
                {
                    if (Random.Range(1, enemy.Length) == 1) break;
                    index++;
                }
                indicator.GetComponent<EnemyIndicator>().enemy = enemy[index];
            }
        }
    }

    private void Lose()
    {
        GameData.score = score;
        Cursor.visible = true;
        SceneManager.LoadScene("LossScreen");
    }

    public void UpdateScore(int change)
    {
        score += change;
        scoreText.text = score.ToString();
    }
}
