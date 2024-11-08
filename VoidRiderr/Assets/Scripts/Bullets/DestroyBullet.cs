using System.Collections;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;

    private void OnEnable()
    {
        StartCoroutine(DestroyBullets());
    }

    private IEnumerator DestroyBullets()
    {
        yield return new WaitForSeconds(timeToDestroy);
        BulletPool.Instance.ReturnBullet(gameObject);
    }
}