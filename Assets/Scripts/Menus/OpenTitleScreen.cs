using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenTitleScreen : MonoBehaviour
{
    public void OnMouseDown()
    {
        GameData.weaponIndex = 0;
        gameObject.GetComponent<AudioSource>().Play();
        new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
