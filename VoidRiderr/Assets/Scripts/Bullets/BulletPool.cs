using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    public GameObject bulletPrefab;
    public int poolSize = 10;

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
    
    public GameObject GetBullet(BulletType.BulletOwner type, Transform spawnTransform)
    {
        if (type != BulletType.BulletOwner.Player)
        {
            Debug.Log("Este pool solo permite balas del jugador.");
            return null;
        }

        foreach (var bullet in _bulletPool)
        {
            BulletType bulletType = bullet.GetComponent<BulletType>();
            if (bulletType.owner == type && !bullet.activeInHierarchy)
            {
                bullet.transform.position = spawnTransform.position;
                bullet.transform.rotation = spawnTransform.rotation;
                bullet.SetActive(true);
                return bullet;
            }
        }

        Debug.Log($"No hay balas disponibles para {type}.");
        return null;
    }


    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
}