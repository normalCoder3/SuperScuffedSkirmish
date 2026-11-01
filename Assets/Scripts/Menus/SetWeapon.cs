using TMPro;
using UnityEngine;

public class SetWeapon : MonoBehaviour
{
    private int index;
    public GameObject weaponText;
    public string[] weaponType;

    void Start()
    {
        index = 0;
    }

    public void OnMouseDown()
    {
        GameData.weaponIndex = index;
        weaponText.GetComponent<TMP_Text>().text = "Using: " + weaponType[index];
        index = index >= 1 ? 0 : index + 1;
    }
}
