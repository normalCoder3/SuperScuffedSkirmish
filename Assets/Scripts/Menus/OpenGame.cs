using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGame : MonoBehaviour
{
    public void OnMouseDown()
    {
        gameObject.GetComponent<AudioSource>().Play();
        GameData.Reset();
        if (gameObject.CompareTag("MeleeWeapon")) {
            GameData.weaponIndex = 1;
        } 
        else if (gameObject.CompareTag("RangedWeapon"))
        {
            GameData.weaponIndex = 0;
        }
        Invoke(nameof(LoadGame), GetComponent<AudioSource>().clip.length);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
