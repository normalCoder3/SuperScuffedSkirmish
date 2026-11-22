using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] enemies;
    private int enemiesDefeated;
    public int enemiesInWave;
    private int enemiesRemaining;
    public TMP_Text enemiesRemainingText;
    public GameObject indicator;
    private float lastSpawnTime;
    private float lastScoreTime;
    private GameObject player;
    private float startTime;
    public float spawnCooldown;
    private int score;
    public int scoreInterval;
    public TMP_Text scoreText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        enemiesRemaining = enemiesInWave;
        enemiesRemainingText.text = enemiesRemaining + " enemies left";
        scoreText.text = "0";
        startTime = Time.time;
    }

    void Update()
    {
        if (player == null || enemiesDefeated == enemiesInWave)
        {
            Camera.main.GetComponent<AudioSource>().Stop();
            Invoke(nameof(EndGame), 1);
        } 
        else
        {
            if (Time.time - lastScoreTime > scoreInterval)
            {
                lastScoreTime = Time.time;
                UpdateScore(100);
            }

            if (enemiesRemaining > 0 && (Time.time - lastSpawnTime > spawnCooldown * Mathf.Pow(0.9999f, Mathf.Pow(Time.time - startTime, 2))))
            {
                enemiesRemaining--;
                lastSpawnTime = Time.time;

                GameObject indicator = Instantiate(this.indicator, GameObject.Find("Enemies").transform);
                indicator.transform.position = new(Random.Range(-6.5f, 11.5f), 24f);

                int index = 0;
                for (int i = 0; i < enemies.Length - 1; i++)
                {    
                    if (Random.Range(1, enemies.Length) == 1) break;
                    index++;
                }
                indicator.GetComponent<EnemyIndicator>().enemy = enemies[index];
            }
        }
    }

    private void EndGame()
    {
        GameData.enemiesDefeated = enemiesDefeated;
        GameData.score = score;
        Cursor.visible = true;
        if (player != null)
        {
            SceneManager.LoadScene("Win");
        }
        else
        {
            SceneManager.LoadScene("Loss");
        }
    }

    public void UpdateEnemyCount(int change)
    {
        enemiesDefeated += change;
        enemiesRemainingText.text = enemiesDefeated == enemiesInWave ? "All clear!" : (enemiesInWave - enemiesDefeated) + " enemies left";
    }

    public void UpdateScore(int change)
    {
        score += change;
        scoreText.text = score.ToString();
    }
}
