using System;
using UnityEngine;
using static Interfaces;

public class RailBehaviour : MonoBehaviour, IFollowPoints
{
    public TransformsToFollow transformsToFollow;
    [Header("Movement and Rotation Settings")]
    [SerializeField] private float speed;
    
    private void Start()
    {
        GetNextPoint();
    }
    private void Update()
    {
        MoveToNextPoint();  
    }
    public virtual void GetNextPoint()
    {
        transformsToFollow.target = transformsToFollow.points[transformsToFollow.index];
        MoveToNextPoint();
        transformsToFollow.index++;
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
            GetNextPoint();
        }
    }


    [System.Serializable]
    public struct TransformsToFollow
    {
        public Transform[] points;
        public Transform target;
        public int index;
    }
}
