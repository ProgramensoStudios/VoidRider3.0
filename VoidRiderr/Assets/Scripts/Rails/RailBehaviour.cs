using System;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena
using static Interfaces;

public class RailBehaviour : MonoBehaviour, IFollowPoints
{
    public TransformsToFollow transformsToFollow;

    [Header("Movement and Rotation Settings")]
    [SerializeField] public float speed;

    private void Start()
    {
        if (transformsToFollow.points.Length > 0)
        {
            GetNextPoint();
        }
        else
        {
            Debug.LogError("El array de puntos está vacío.");
        }
    }

    private void Update()
    {
        if (transformsToFollow.index < transformsToFollow.points.Length)
        {
            MoveToNextPoint();
        }
    }

    public virtual void GetNextPoint()
    {
        if (transformsToFollow.index < transformsToFollow.points.Length)
        {
            transformsToFollow.target = transformsToFollow.points[transformsToFollow.index];
        }
        else
        {
            OnReachEndOfPoints();
        }
    }

    public virtual void MoveToNextPoint()
    {
        float dist = Vector3.Distance(transformsToFollow.target.position, transform.position);

        if (dist >= 1f)
        {
            // Movimiento
            var direction = (transformsToFollow.target.position - transform.position).normalized;
            transform.position += speed * Time.deltaTime * direction;
        }
        else
        {
            transformsToFollow.index++;
            GetNextPoint();
        }
    }

    private void OnReachEndOfPoints()
    {
        SceneManager.LoadScene("WinScene"); 
    }

    [Serializable]
    public struct TransformsToFollow
    {
        public Transform[] points;
        public Transform target;
        public int index;
    }
}

