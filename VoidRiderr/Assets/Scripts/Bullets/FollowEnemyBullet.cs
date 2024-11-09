
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowEnemyBullet : MonoBehaviour
{
    public float speed;
    public float detectionRadius; 
   [SerializeField] private Transform _target;
   private Enemy _enemy;

   [SerializeField] public int damage;
   
    
    private Collider[] enemiesInRange = new Collider[10]; 
    private SpawnPosReference _spawnRef;
    [SerializeField] private Transform spawnPos;

    private void Awake()
    {
        _spawnRef = FindAnyObjectByType<SpawnPosReference>();
        spawnPos = _spawnRef.transform;
    }

    private void Update()
    {
        _target = FindClosestEnemy();
        
        if (_target != null && _target.CompareTag("Enemy")) 
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
        
            if (Vector3.Distance(transform.position, _target.position) < 0.1f)
            {
                BulletPool.Instance.ReturnBullet(gameObject);
            }
        }
        else
        {
            // Mueve el objeto hacia adelante de forma local
            transform.localPosition += transform.localRotation * Vector3.forward * (speed * Time.deltaTime);
        }
    }

    private Transform FindClosestEnemy()
    {
        var enemyLayerMask = 1 << 7;
        var numEnemies = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, enemiesInRange, enemyLayerMask);
    
        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;
    
        for (int i = 0; i < numEnemies; i++)
        {
            if (enemiesInRange[i].gameObject.layer == 7)
            {
                Transform enemy = enemiesInRange[i].transform;
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.position);

                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    closestEnemy = enemy;
                }
            }
        }
        return closestEnemy;
    }


    private void OnTriggerEnter(Collider other)
    {
        //Layer 7 es Enemy!
        if (other.gameObject.layer != 7) return;
       // BulletPool.Instance.ReturnBullet(gameObject);
        _enemy = other.GetComponent<Enemy>();
        _enemy.TakeDamage(damage);
    }
    
    private void OnDrawGizmosSelected()
    {
        // Dibuja el radio de detección en la escena para visualización
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnEnable()
    {
        transform.position = spawnPos.position;
        transform.rotation = spawnPos.rotation;
    }
    
}