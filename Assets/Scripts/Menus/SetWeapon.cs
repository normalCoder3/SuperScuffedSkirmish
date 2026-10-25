using TMPro;
using UnityEngine;

public class SetWeapon : MonoBehaviour
{
    public string[] weapons;
    public GameObject weaponText;
    private int index;

    void Start()
    {
        index = 0;
    }

    public void OnMouseDown()
    {
        index = index >= weapons.Length - 1 ? 0 : index + 1;
        GameData.weapon = weapons[index];
        weaponText.GetComponent<TMP_Text>().text = "Using: " + weapons[index];
    }
}
