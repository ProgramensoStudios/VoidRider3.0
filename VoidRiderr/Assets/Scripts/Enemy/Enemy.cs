using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public bool canShoot;
    [SerializeField] private PoolManager bulletPool;
    [SerializeField] private float timeToShoot;
    [SerializeField] protected GameObject particleDestroy;
    protected AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public virtual void TakeDamage(int damage)
    {
        health-= damage;
    }

    public void Shoot()
    {
        StartCoroutine(ShootCor());
        Debug.Log(gameObject.transform.GetChild(0).name);
    }

    private IEnumerator ShootCor()
    {
        while (canShoot)
        {
            bulletPool.AskForObject(gameObject.transform.GetChild(0));
            yield return new WaitForSeconds(timeToShoot);
        }
    }
}
