using System.Threading;
using UnityEngine;

public class LookAtYOnly : MonoBehaviour
{
    public Transform target;
    public bool playerLocated;

    void Update()
    {
        if (!playerLocated) return;
        // Calcula la dirección hacia el objetivo, ignorando la altura
        Vector3 direction = target.position - transform.localPosition;
        //sVector3 targetVector = new Vector3(0, direction.y, 0);
        gameObject.transform.LookAt(target, Vector3.left);
    }

    public void LookAt(Transform player)
    {
        playerLocated = true;
        target = player;
    }

    public void StopLooking()
    {
        playerLocated = false;
    }
}