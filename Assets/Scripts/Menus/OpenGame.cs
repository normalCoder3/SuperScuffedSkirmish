using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGame : MonoBehaviour
{
    public void OnMouseDown()
    {
        GameData.score = 0;
        gameObject.GetComponent<AudioSource>().Play();
        WaitForSeconds pause = new(1);
        SceneManager.LoadScene(1);
    }
}
