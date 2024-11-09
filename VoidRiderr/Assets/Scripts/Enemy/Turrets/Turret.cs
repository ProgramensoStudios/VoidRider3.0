using System;
using System.Collections;
using UnityEngine;

public class Turret : Enemy
{
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
        if(health<=0)
        {
            AudioManager.Instance.InstanceParticles(transform, particleDestroy);
            AudioManager.Instance.PlayAudio(audioSource);
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
