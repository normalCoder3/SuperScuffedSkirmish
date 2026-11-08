using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGame : MonoBehaviour
{
    public void OnMouseDown()
    {
        GameData.score = 0;
        gameObject.GetComponent<AudioSource>().Play();
        Invoke(nameof(LoadGame), GetComponent<AudioSource>().clip.length);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
