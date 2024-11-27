using System;
using UnityEngine;

public class ZoneDeactivate : MonoBehaviour
{
    public Turret[] enemies;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6) return;
        for (int indexEnemy = 0; indexEnemy < enemies.Length; indexEnemy++)
        {
            if (enemies[indexEnemy]  != null)
            {
                enemies[indexEnemy].canShoot = false;
            }
        }
    }
}
