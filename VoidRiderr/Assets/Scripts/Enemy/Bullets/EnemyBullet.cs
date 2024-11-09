using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    [SerializeField]private Transform originalParent;
    private Vector3 originalPosition;

    private void OnEnable()
    {
        // Asigna el parent original solo si no se ha establecido previamente
        if (originalParent == null)
        {
            originalParent = transform.parent;
            originalPosition = transform.localPosition;
        }

        // Restablece la posición y rotación del objeto cuando se activa
        transform.SetParent(originalParent);
        transform.rotation = originalParent.rotation;
        transform.localPosition = originalPosition;
        
        StartCoroutine(WaitToDetach());
    }

    private void Update()
    {
        // Movimiento hacia adelante en espacio local
        transform.localPosition += transform.localRotation * Vector3.forward * (speed * Time.deltaTime);
    }

    private IEnumerator WaitToDetach()
    {
        yield return new WaitForSeconds(0.5f);
        transform.parent = null;
    }
}