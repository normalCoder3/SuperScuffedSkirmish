using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenWeapons : MonoBehaviour
{
    public void OnMouseDown()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Invoke(nameof(LoadWeapons), GetComponent<AudioSource>().clip.length);
    }

    private void LoadWeapons()
    {
        SceneManager.LoadScene("Weapons");
    }
}
