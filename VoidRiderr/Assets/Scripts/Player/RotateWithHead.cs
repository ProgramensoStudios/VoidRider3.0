using UnityEngine;

public class RotateWithHead : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Obtén la referencia de la cámara principal
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera == null) return;

        // Obtiene la rotación global de la cámara en los ejes X e Y, ignorando Z si no es necesario
        Vector3 targetRotation = mainCamera.transform.eulerAngles;
        transform.rotation = Quaternion.Euler(targetRotation.x, targetRotation.y, 0); // Ignora Z o usa targetRotation.z si lo necesitas
    }
}
