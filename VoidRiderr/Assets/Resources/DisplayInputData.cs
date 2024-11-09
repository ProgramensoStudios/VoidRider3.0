using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

[RequireComponent(typeof(InputData))]
public class DisplayInputData : MonoBehaviour
{
    private InputData _inputData;
    [Header ("Nave Objects")]
    [SerializeField] private Rigidbody cabinRigidbody;

    
    [Header ("Speed & Vectors 3")]
   // [SerializeField] public float rotSpeed = 0.002f;
    [SerializeField] private Vector3 relativeFwd;
    [SerializeField] public float speed;
    [SerializeField] private Vector3 testConstraint;

    [SerializeField] private BulletPool bulletPool;

    [SerializeField]private bool _canShoot;
  // [SerializeField] Vector3 spinMovement;

    
    private void Start()
    {
        _inputData = GetComponent<InputData>();
        //bulletPool = FindAnyObjectByType<BulletPool>();
    }

    private void Update()
    {
        /*
        // Rotation
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out var rightAxis))
        {
            spinMovement = new Vector3(rightAxis.y * rotSpeed * -1f * Time.deltaTime, rightAxis.x * rotSpeed * Time.deltaTime, 0);
        }
        
        //rot
        var newRot = transform.localEulerAngles + spinMovement;
        newRot.x = Mathf.Clamp(newRot.x, -15, 15);
        newRot.y = Mathf.Clamp(newRot.y, -15, 15);
        newRot.z = 0;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(newRot), Time.deltaTime * 5f);
        */
        
        //Movevent
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out var leftAxis))
        {
            var direction = new Vector3(leftAxis.x, leftAxis.y, 0f);

            if (direction.magnitude > 1f)
            {
                direction.Normalize();
            }
            relativeFwd = cabinRigidbody.transform.TransformDirection(direction);
            cabinRigidbody.linearVelocity = relativeFwd * speed;
        }
        //movement
        var vector = transform.localPosition;
        vector.x = Mathf.Clamp(vector.x, -testConstraint.x, testConstraint.x);
        vector.y = Mathf.Clamp(vector.y, -testConstraint.y, testConstraint.y);
        vector.z = Mathf.Clamp(vector.z, -testConstraint.z, testConstraint.z);
        transform.localPosition = vector;
        
        //Shoot
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.trigger, out var rightTrigger))
        {
            if (rightTrigger > 0 && _canShoot)
            {
                _canShoot = false;
                var currentBullet = BulletPool.Instance.GetBullet();
                currentBullet.transform.parent = null;
                StartCoroutine(ReadyToShoot());
            }
        }
    }

    private IEnumerator ReadyToShoot()
    {
        yield return new WaitForSeconds(0.2f);
        _canShoot = true;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(this.transform.parent.position, testConstraint);
    }

}
