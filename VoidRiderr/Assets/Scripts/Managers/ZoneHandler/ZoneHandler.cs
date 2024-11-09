using System;
using UnityEngine;

public class ZoneHandler : MonoBehaviour
{
    public GameObject[] enemiesInZone;
    public GameObject player;

    private void GetTurretsShooting()
    {
        for (var indexEnemies = 0; indexEnemies < enemiesInZone.Length; indexEnemies++)
        {
            var enemy = enemiesInZone[indexEnemies].GetComponent<Turret>();
            var yRot = enemiesInZone[indexEnemies].GetComponentInChildren<LookAtYOnly>();
            yRot.LookAt(player.transform);
            enemy.canShoot = true;
            enemy.Shoot();
        }
    }
    private void GetTurretsStopShooting()
    {
        for (var indexEnemies = 0; indexEnemies < enemiesInZone.Length; indexEnemies++)
        {
            var enemy = enemiesInZone[indexEnemies].GetComponent<Turret>();
            var yRot = enemiesInZone[indexEnemies].GetComponentInChildren<LookAtYOnly>();
            yRot.StopLooking();
            enemy.canShoot = false;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6) return;
        {
            GetTurretsShooting();
            player = other.gameObject;
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 6) return;
        {
            GetTurretsStopShooting();
        }
    }
}
