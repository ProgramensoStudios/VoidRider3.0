using System;
using System.Collections;
using UnityEngine;

public class Turret : Enemy
{
    private bool _isAlive = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 9) return;
        var currentDamage = other.GetComponent<FollowEnemyBullet>().damage;
        TakeDamage(currentDamage);
        BulletPool.Instance.ReturnBullet(other.gameObject);
        DestroyCompare();
    }

    private void DestroyCompare()
    {
        if (health > 0 || !_isAlive) return;
        //canShoot = false;
        AudioManager.Instance.InstanceParticles(transform, particleDestroy);
        AudioManager.Instance.PlayAudio(audioSource);
        _isAlive = false;
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        canShoot = false;
    }
}
