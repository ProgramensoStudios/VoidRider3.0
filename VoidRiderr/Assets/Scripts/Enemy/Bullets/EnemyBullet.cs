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
    [SerializeField] private float yOffset; // Ajuste de altura.

    [SerializeField] private Transform target;
    [SerializeField] private float detectionRadius;

    private Coroutine destroyCoroutine;
    private Coroutine detachCoroutine;

    private void Awake()
    {
        _originalPosition = transform.localPosition;
        target = FindAnyObjectByType<DoorReference>().gameObject.transform;
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
        // Detenemos las corrutinas si el objeto se desactiva.
        if (detachCoroutine != null)
        {
            StopCoroutine(detachCoroutine);
            detachCoroutine = null;
        }

        if (destroyCoroutine != null)
        {
            StopCoroutine(destroyCoroutine);
            destroyCoroutine = null;
        }
    }

    private void Update()
    {
        if (IsTargetInDetectionRadius())
        {
            // Aplica el yOffset al mover hacia el target.
            Vector3 targetPosition = target.position;
            targetPosition.y -= yOffset; // Ajusta la posición objetivo con el offset.

            // Mueve hacia la posición ajustada.
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Verifica si está lo suficientemente cerca del target ajustado.
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                OnHitTarget();
            }
        }
        else
        {
            // Movimiento original hacia adelante.
            transform.localPosition += transform.localRotation * Vector3.forward * (speed * Time.deltaTime);
        }
    }

    private bool IsTargetInDetectionRadius()
    {
        // Comprueba la distancia al target original.
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
        // Lógica para cuando impacta al target.
        gameObject.SetActive(false);
        Debug.Log("Ouch!");
    }

    private IEnumerator WaitToDetach()
    {
        yield return new WaitForSeconds(0.3f);

        // Verifica si el objeto sigue activo antes de asignar null al parent.
        if (gameObject.activeInHierarchy)
        {
            transform.parent = null;
        }
    }

    private IEnumerator DestroyBullets()
    {
        yield return new WaitForSeconds(timeToDestroy);

        // Verifica si el objeto sigue activo antes de desactivarlo.
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (target == null) return;

        // Dibuja un radio de detección.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Dibuja la posición ajustada con el offset en el eje Y.
        Gizmos.color = Color.red;
        Vector3 adjustedTarget = target.position;
        adjustedTarget.y -= yOffset;
        Gizmos.DrawLine(transform.position, adjustedTarget);
    }
}
