using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject bulletPrefab;
    public int poolSize = 10;
    public bool isPlayerBullet;

    private Queue<GameObject> _bulletPool = new Queue<GameObject>();

    [SerializeField] private Transform spawnTransform;
    [SerializeField] private Transform posParent;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnTransform);
            if (isPlayerBullet)
            {
                bullet.transform.SetParent(posParent);
            }
            else
            {
                bullet.transform.SetParent(null);
            }
            bullet.SetActive(false);
            _bulletPool.Enqueue(bullet);
        }
    }
    
    public GameObject GetBullet()
    {
        if (_bulletPool.Count > 0)
        {
            GameObject bullet = _bulletPool.Dequeue();
            if (isPlayerBullet)
            {
                bullet.transform.SetParent(posParent);
            }
            else
            {
                bullet.transform.SetParent(null);
            }
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            Debug.Log("Sin Balas!");
            return null;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        if (isPlayerBullet)
        {
            bullet.transform.SetParent(posParent);
        }
        else
        {
            bullet.transform.SetParent(null);
        }
        _bulletPool.Enqueue(bullet);
    }
}