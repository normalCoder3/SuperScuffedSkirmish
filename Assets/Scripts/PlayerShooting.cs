using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Projectile prefab to shoot")]
    public GameObject projectilePrefab;

    public int projectileSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.position;
        }
    }
}
