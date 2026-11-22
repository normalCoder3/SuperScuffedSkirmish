using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenTitle : MonoBehaviour
{
    public void OnMouseDown()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Invoke(nameof(LoadTitleScreen), GetComponent<AudioSource>().clip.length);
    }

    private void LoadTitleScreen()
    {
        SceneManager.LoadScene("Title");
    }
}
