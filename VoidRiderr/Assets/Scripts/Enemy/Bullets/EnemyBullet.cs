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
        transform.SetParent(originalParent);
        transform.rotation = originalParent.rotation;
        transform.localPosition = _originalPosition;

        StartCoroutine(WaitToDetach());
        StartCoroutine(DestroyBullets());
    }

    private void OnDisable()
    {
        if (transform.parent != null)
        {
            originalParent = transform.parent;
        }

        if (originalParent == null)
        {
            Debug.Log("El objeto no tiene un parent asignado al habilitarse.");
            return;
        }
        
        transform.SetParent(originalParent);
        transform.rotation = originalParent.rotation;
        transform.localPosition = _originalPosition;
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