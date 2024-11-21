using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    [SerializeField] private float movementRatio = 10.0f; // Escala del movimiento
    [SerializeField] private float frequency = 1.0f; // Frecuencia de oscilación
    [SerializeField] private Movement moveDirection = Movement.X;

    [System.Serializable]
    public enum Movement
    {
        X,
        Y,
        Z
    }

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position; // Guarda la posición inicial
    }

    private void Update()
    {
        // Calcula el desplazamiento escalado con frecuencia y rango
        float offset = Mathf.Sin(Time.time * frequency) * movementRatio;

        switch (moveDirection)
        {
            case Movement.X:
                transform.position = new Vector3(initialPosition.x + offset, initialPosition.y, initialPosition.z);
                break;
            case Movement.Y:
                transform.position = new Vector3(initialPosition.x, initialPosition.y + offset, initialPosition.z);
                break;
            case Movement.Z:
                transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z + offset);
                break;
        }
    }
}
