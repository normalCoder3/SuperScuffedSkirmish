using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGame : MonoBehaviour
{
    public void OnMouseDown()
    {
        gameObject.GetComponent<AudioSource>().Play();
        GameData.Reset();
        Invoke(nameof(LoadGame), GetComponent<AudioSource>().clip.length);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
