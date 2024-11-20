using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class LaserMovement : MonoBehaviour
{
    [SerializeField] private float movementRatio;
    

    private void Update()
    {
       transform.position = new Vector3(transform.parent.position.x, Mathf.Sin(Time.time) * movementRatio, transform.localPosition.z);
    }
}
