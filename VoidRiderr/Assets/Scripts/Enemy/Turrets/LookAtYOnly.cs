using System.Threading;
using UnityEngine;

public class LookAtYOnly : MonoBehaviour
{
    public Transform target;
    public bool playerLocated;
    [SerializeField] private float rotationSpeed; // Velocidad de rotación suavizada.

    void Update()
    {
        if (!playerLocated) return;

        // Calcula la dirección hacia el objetivo.
        Vector3 directionToTarget = target.position - transform.position;

        // Suaviza la dirección usando Lerp.
        Vector3 smoothedDirection = Vector3.Lerp(transform.forward, directionToTarget.normalized, Time.deltaTime * rotationSpeed);

        // Calcula la nueva rotación a partir de la dirección suavizada.
        Quaternion targetRotation = Quaternion.LookRotation(smoothedDirection);

        // Aplica la rotación suavizada.
        transform.rotation = targetRotation;
    }

  /*  public void LookAt(Transform player)
    {
        playerLocated = true;
        target = player;
    }

    public void StopLooking()
    {
        playerLocated = false;
    }*/
}