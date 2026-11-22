using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    public GameObject titleMessage;
    public string[] messages;
    public GameObject enemiesDefeatedText;
    public GameObject scoreText;

    void Start()
    {
        titleMessage.GetComponent<TMP_Text>().text = messages[Random.Range(0, messages.Length)];
        enemiesDefeatedText.GetComponent<TMP_Text>().text = "Enemies defeated: " + GameData.enemiesDefeated;
        scoreText.GetComponent<TMP_Text>().text = "Score: " + GameData.score;
    }
}
