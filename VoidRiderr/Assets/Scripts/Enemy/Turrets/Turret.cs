using System;
using System.Collections;
using UnityEngine;

public class Turret : Enemy
{
    private bool isAlive = true;
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            int currentDamage = other.GetComponent<FollowEnemyBullet>().damage;
            TakeDamage(currentDamage);
            BulletPool.Instance.ReturnBullet(other.gameObject);
            DestroyCompare();
        }
    }

    private void DestroyCompare()
    {
        if(health<=0 && isAlive)
        {
            canShoot = false;
            AudioManager.Instance.InstanceParticles(this.transform, particleDestroy);
            AudioManager.Instance.PlayAudio(audioSource);
            isAlive = false;
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
