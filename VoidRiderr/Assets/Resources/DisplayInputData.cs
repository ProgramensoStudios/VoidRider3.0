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
    [SerializeField] private Vector3 relativeFwd;
    [SerializeField] public float speed;
    [SerializeField] private Vector3 testConstraint;
    [SerializeField] protected Transform spawnPos;
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private RailBehaviour rails;
    [SerializeField]private bool _canShoot;

    Coroutine cor;
    
    private void Start()
    {
        _inputData = GetComponent<InputData>();
    }

    private void Update()
    {
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
        vector.y = Mathf.Clamp(vector.y,  0, testConstraint.y);
        vector.z = Mathf.Clamp(vector.z, -testConstraint.z, testConstraint.z);
        transform.localPosition = vector;
        
        //Shoot
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.triggerButton, out var rightTrigger))
        {
            if (rightTrigger && _canShoot)
            {
                if (_canShoot)
                {
                    _canShoot = false;
                    var currentBullet = BulletPool.Instance.GetBullet(BulletType.BulletOwner.Player, spawnPos);
                    currentBullet.transform.parent = null;
                    if (cor != null) StopCoroutine(cor);
                    cor = StartCoroutine(ReadyToShoot());
                }
                else
                {
                    Debug.Log("ESTO NO DEBERIA ESTAR PASANDO");
                }
            }
        }

        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.gripButton, out var rightGrip)
            &&
            (_inputData._leftController.TryGetFeatureValue(CommonUsages.gripButton, out var leftGrip)))
        {
            if (rightGrip && leftGrip)
            {
                rails.speed = 120;
            }
            else if (!rightGrip && !leftGrip)
            {
                rails.speed = 80;
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
        Gizmos.DrawWireCube(transform.parent.position, testConstraint * 2);
    }

}
