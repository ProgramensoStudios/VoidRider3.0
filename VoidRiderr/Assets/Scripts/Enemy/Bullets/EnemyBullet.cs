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
    [SerializeField] private float yOffset;
    [SerializeField] private Transform target;
    [SerializeField] private float detectionRadius;
    private Coroutine destroyCoroutine;
    private Coroutine detachCoroutine;
    private Transform enemyParent;
    private void Awake()
    {
        _originalPosition = transform.localPosition;
        target = FindAnyObjectByType<DoorReference>().gameObject.transform;
        enemyParent = FindAnyObjectByType<EnemyPoolRef>().gameObject.transform;
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

        if (!gameObject.activeInHierarchy) return;
        detachCoroutine = StartCoroutine(WaitToDetach());
        destroyCoroutine = StartCoroutine(DestroyBullets());
    }

    private void OnDisable()
    {
        if (detachCoroutine != null)
        {
            StopCoroutine(detachCoroutine);
            detachCoroutine = null;
        }

        if (destroyCoroutine == null) return;
        StopCoroutine(destroyCoroutine);
        destroyCoroutine = null;
    }

    private void Update()
    {
        if (IsTargetInDetectionRadius())
        {
            Vector3 targetPosition = target.position;
            targetPosition.y -= yOffset;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                OnHitTarget();
            }
        }
        else
        {
            transform.localPosition += transform.localRotation * Vector3.forward * (speed * Time.deltaTime);
        }
    }

    private bool IsTargetInDetectionRadius()
    {
        return target != null && Vector3.Distance(transform.position, target.position) <= detectionRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            OnHitTarget();
        }
    }

    private void OnHitTarget()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator WaitToDetach()
    {
        yield return new WaitForSeconds(0.1f);
        
        if (gameObject.activeInHierarchy)
        {
            transform.SetParent(enemyParent);
        }
    }

    private IEnumerator DestroyBullets()
    {
        yield return new WaitForSeconds(timeToDestroy);
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (target == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Vector3 adjustedTarget = target.position;
        adjustedTarget.y -= yOffset;
        Gizmos.DrawLine(transform.position, adjustedTarget);
    }
}
