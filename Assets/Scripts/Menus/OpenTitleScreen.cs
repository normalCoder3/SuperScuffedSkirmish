using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenTitleScreen : MonoBehaviour
{
    public void OnMouseDown()
    {
        GameData.weaponIndex = 0;
        gameObject.GetComponent<AudioSource>().Play();
        Invoke(nameof(LoadTitleScreen), GetComponent<AudioSource>().clip.length);
    }

    private void LoadTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
