using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void OnMouseDown()
    {
        SceneManager.LoadScene("Level");
    }
}
