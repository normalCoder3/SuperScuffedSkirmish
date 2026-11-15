   using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    public void OnMouseDown()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Invoke(nameof(LoadCredits), GetComponent<AudioSource>().clip.length);
    }

    private void LoadCredits()
    {
        SceneManager.LoadScene(4);
    }
}
