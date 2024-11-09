using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject bulletPrefab;
    public int poolSize = 3;

    private Queue<GameObject> _bulletPool = new Queue<GameObject>();

    [SerializeField] private Transform spawnTransform;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnTransform);
            bullet.SetActive(false);
            _bulletPool.Enqueue(bullet);
        }
    }
    
    public GameObject GetBullet()
    {
        if (_bulletPool.Count > 0)
        {
            GameObject bullet = _bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            Debug.Log("Sin Balas! ");
            return null;
        }
        // else
        // {
        //     // Si el pool está vacío, se puede crear una nueva bala (opcional)
        //     GameObject bullet = Instantiate(bulletPrefab);
        //     return bullet;
        // }
    }

    public void ReturnBullet(GameObject bullet)
    {
        Debug.Log("Bala regresando");
        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
}