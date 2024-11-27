using System;
using UnityEngine;

public class RailBehaviour : MonoBehaviour
{
    public TransformsToFollow transformsToFollow;

    [Header("Movement and Rotation Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    private int segmentIndex = 0;
    private float t = 0f; // Controla el progreso entre dos puntos.

    private void Update()
    {
        if (transformsToFollow.points.Length < 2) return;

        // Calcula los puntos de la curva usando Catmull-Rom
        Vector3 p0 = transformsToFollow.points[Mathf.Clamp(segmentIndex - 1, 0, transformsToFollow.points.Length - 1)].position;
        Vector3 p1 = transformsToFollow.points[segmentIndex].position;
        Vector3 p2 = transformsToFollow.points[Mathf.Clamp(segmentIndex + 1, 0, transformsToFollow.points.Length - 1)].position;
        Vector3 p3 = transformsToFollow.points[Mathf.Clamp(segmentIndex + 2, 0, transformsToFollow.points.Length - 1)].position;

        // Interpolaci�n c�bica para suavizar
        Vector3 position = CatmullRom(p0, p1, p2, p3, t);
        transform.position = position;

        // Rotaci�n hacia el siguiente punto
        Vector3 direction = (p2 - transform.position).normalized;
        if (direction.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Incrementa t basado en la velocidad
        t += Time.deltaTime * speed / Vector3.Distance(p1, p2);

        // Si llegamos al final del segmento, pasa al siguiente
        if (t >= 1f)
        {
<<<<<<< HEAD
            GetNextPoint();
=======
            t = 0f;
            segmentIndex++;
            if (segmentIndex >= transformsToFollow.points.Length - 1)
            {
                segmentIndex = 0; // Reinicia el ciclo o det�n el movimiento
    }

    // Funci�n para la interpolaci�n Catmull-Rom
    private Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float t2 = t * t;

        return 0.5f * (
            (2 * p1) +
            (-p0 + p2) * t +
            (2 * p0 - 5 * p1 + 4 * p2 - p3) * t2 +
            (-p0 + 3 * p1 - 3 * p2 + p3) * t3
        );
    }

    [System.Serializable]
    public struct TransformsToFollow
    {
        public Transform[] points;
    }
}

