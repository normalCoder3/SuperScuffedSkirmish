using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenTitleScreen : MonoBehaviour
{
    public void OnMouseDown()
    {
        GameData.score = 0;
        gameObject.GetComponent<AudioSource>().Play();
        WaitForSeconds pause = new(1);
        SceneManager.LoadScene(0);
    }
}
