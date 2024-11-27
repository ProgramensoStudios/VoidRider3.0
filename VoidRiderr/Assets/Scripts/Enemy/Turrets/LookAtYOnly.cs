using System.Threading;
using UnityEngine;

public class LookAtYOnly : MonoBehaviour
{
    public Transform target;
    public bool playerLocated;
    [SerializeField] private float rotationSpeed; 

    void Update()
    {
        if (!playerLocated) return;
        
        Vector3 directionToTarget = target.position - transform.position;
        Vector3 smoothedDirection = Vector3.Lerp(transform.forward, directionToTarget.normalized, Time.deltaTime * rotationSpeed);
        Quaternion targetRotation = Quaternion.LookRotation(smoothedDirection);
        transform.rotation = targetRotation;
    }
}