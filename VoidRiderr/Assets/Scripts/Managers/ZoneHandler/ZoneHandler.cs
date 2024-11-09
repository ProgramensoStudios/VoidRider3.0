using System;
using UnityEngine;

public class ZoneHandler : MonoBehaviour
{
    public GameObject[] enemiesInZone;

    private void GetTurretsShooting()
    {
        for (var indexEnemies = 0; indexEnemies < enemiesInZone.Length; indexEnemies++)
        {
            var enemy = enemiesInZone[indexEnemies].GetComponent<Turret>();
            enemy.canShoot = true;
            enemy.Shoot();
        }
    }
    private void GetTurretsStopShooting()
    {
        for (var indexEnemies = 0; indexEnemies < enemiesInZone.Length; indexEnemies++)
        {
            var enemy = enemiesInZone[indexEnemies].GetComponent<Turret>();
            enemy.canShoot = false;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6) return;
        {
            Debug.Log("STAY");
            
            GetTurretsShooting();
        }
    }
    

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer != 6) return;
        {
            GetTurretsStopShooting();
        }
    }
}
