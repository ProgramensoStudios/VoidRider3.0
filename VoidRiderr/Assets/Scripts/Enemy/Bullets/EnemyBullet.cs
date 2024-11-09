using System;
using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    private Transform originalParent;
    private Vector3 _originalPosition;
    [SerializeField] private float timeToDestroy;
    public int damage;

    private void Awake()
    {
        _originalPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        if (originalParent == null)
        {
            originalParent = transform.parent;
            transform.SetParent(originalParent);
            return;
        }
        transform.SetParent(originalParent);
        transform.rotation = originalParent.rotation;
        transform.localPosition = _originalPosition;

        StartCoroutine(WaitToDetach());
        StartCoroutine(DestroyBullets());
    }

   

    private void Update()
    {
        transform.localPosition += transform.localRotation * Vector3.forward * (speed * Time.deltaTime);
    }

    private IEnumerator WaitToDetach()
    {
        yield return new WaitForSeconds(0.5f);
        transform.parent = null; 
    }

    private IEnumerator DestroyBullets()
    {
        yield return new WaitForSeconds(timeToDestroy);
        gameObject.SetActive(false);
    }
}