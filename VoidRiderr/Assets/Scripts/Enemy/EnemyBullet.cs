using System;
using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    [SerializeField] private Transform spawnPos;
    
    private void Update()
    {
        transform.position += transform.forward * (speed* Time.deltaTime);
    }
    
    private void OnEnable()
    {
        spawnPos = gameObject.transform.parent;
        transform.position = spawnPos.position;
        transform.position = new Vector3(0, 0, 0);

        StartCoroutine(WaitToDetach());
    }

    private IEnumerator WaitToDetach()
    {
        yield return new WaitForSeconds(0.5f);
        transform.parent = null;

    }
}
