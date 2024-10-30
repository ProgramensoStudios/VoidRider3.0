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
    [SerializeField] private GameObject nave;
    [SerializeField] private Rigidbody cabinRigidbody;

    [Header("Back Camera UI")] [SerializeField]
    private GameObject uiCam;
    
    [Header ("Speed & Vectors 3")]
    [SerializeField] public float rotSpeed = 0.002f;
    [SerializeField] private Vector3 relativeFwd;
    [SerializeField] public float speed;
    [SerializeField] public float turboSpeed;
    [SerializeField] public float turboTime;
    [SerializeField] public float slowmoTime;

    [Header("Turbo")] 
    //[SerializeField] private Turbo turbo;
    private Coroutine _corTurbo;
    
    [Header ("SlowMo")]
    public Coroutine corSlowmo;
   // public SlowMoLoader _slowMoClass;
    
    [Header ("Heavy Bullets")]
    //public BulletLoader bulletClass;
    [SerializeField] private GameObject heavyBullet; 
    [SerializeField] public float rechargeTimer;
    [SerializeField] public bool canShootLeft2 = true;
    [SerializeField] public bool canShootRight2 = true;
    //public BulletLoader bulletLoader;
    
    [Header("Bullets")]
    [SerializeField] private GameObject bullet; 
    [SerializeField] private Transform shootFromRight;
    [SerializeField] private Transform shootFromLeft;
    [SerializeField] private bool canShootLeft1 = true;
    [SerializeField] private bool canShootRight1 = true;

    [Header("PauseManager")] [SerializeField]
    //private HandlePauseUI handlePauseUI;
    private IEnumerator _corCanPlay;

    public bool _canPause;

    
    private DisplayInputData _displayInputData;
    //private DisplayInputDataUI _displayDataUI;
   
    
    private void Start()
    {
        _displayInputData = GetComponent<DisplayInputData>();
        //_displayDataUI = GetComponent<DisplayInputDataUI>();
        _inputData = GetComponent<InputData>();
        corSlowmo = null; 
        uiCam.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {

        //Rotation
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out var rightAxis))
        {
            Quaternion spinMovement = new Quaternion(rightAxis.y * rotSpeed * -1, rightAxis.x * rotSpeed, 0, 1);

            nave.transform.rotation = nave.transform.rotation * spinMovement;
        }

        //Movement

        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out var leftAxis))
        {
            if (leftAxis.y >= 0f)
            {
                relativeFwd = cabinRigidbody.transform.TransformDirection(Vector3.forward);
                cabinRigidbody.linearVelocity = relativeFwd * speed * leftAxis.y;
            }
        }

        //Handle Turbo
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out var rightGiroscope))
        {
            if ((rightGiroscope.x > 0) && (_corTurbo == null))
            {
                StartCoroutine(Turbo());
                Debug.Log("turbo");
                _corTurbo = StartCoroutine(Turbo());
                // sfx.Play();
            }

        }

        // Trigger Shoot Handler

        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.trigger, out var leftTrigger))
        {
            if ((leftTrigger >= 1) && canShootLeft1)
            {
                StartCoroutine(ShootLeft1());
            }
        }

        
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.trigger, out var rightTrigger))
        {
            if ((rightTrigger >= 1) && canShootRight1)
            {
                StartCoroutine(ShootRight1());
            }
        }
        
        //Heavy Shooting
        
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out var leftPrimaryBtn))
        {
            if (leftPrimaryBtn && canShootLeft2 )
            {
                StartCoroutine(ShootLeft2());
            }
        }

        
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out var rightPrimaryBtn))
        {
            if (rightPrimaryBtn && canShootRight2)
            {
                StartCoroutine(ShootRight2());
            }
        }

        // Grip Slowmo Handler

        if ((_inputData._leftController.TryGetFeatureValue(CommonUsages.grip, out var leftGrip)) &&
            (_inputData._rightController.TryGetFeatureValue(CommonUsages.grip, out var rightGrip)))
        {
            if ((leftGrip >= 0.9f) && (rightGrip >= 0.9f) && (corSlowmo == null))
            {
                corSlowmo = StartCoroutine(SlowMo());
            }
        }
        
        
        // Cam Checker
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out var leftJoyClick))
        {
            if (leftJoyClick)
            {
                uiCam.SetActive(true);
            }
            else
            {
                uiCam.SetActive(false);
            }
        }
        
        // Menu

        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.menuButton, out bool menu))
        {
            if (menu)
            {
                _displayInputData.enabled = false;
                //_displayDataUI.enabled = true;
                //handlePauseUI.Pause();
            }
        }

        //Coroutines
        IEnumerator SlowMo()
        {
            //_slowMoClass.circle.fillAmount = 0f;
            Time.timeScale = 0.5f;
            yield return new WaitForSeconds(slowmoTime);
            Time.timeScale = 1f;
            //_slowMoClass.LoadSlowMo();
        }
        
        // Turbo
        IEnumerator Turbo()
        {
            speed = turboSpeed;
            //turbo.TurboPressed();
            yield return new WaitForSeconds(turboTime);
            speed = 50f;
           _corTurbo = null;
           //_slowMoClass.loading = true;
        }

        // Shoot
        IEnumerator ShootRight1()
        {
            Instantiate(bullet, shootFromRight);
            canShootRight1 = false;
            yield return new WaitForSeconds(0.5f);
            canShootRight1 = true;

        }
        
        IEnumerator ShootLeft1()
        {
            Instantiate(bullet, shootFromLeft);
            canShootLeft1 = false;
            yield return new WaitForSeconds(0.5f);
            canShootLeft1 = true;
        }
        
        IEnumerator ShootRight2()
        {
            //bulletLoader.circleRight.fillAmount = 0;
            Instantiate(heavyBullet, shootFromRight);
            canShootRight2 = false;
            //bulletLoader.LoadBullet();
            yield return new WaitForSeconds(rechargeTimer);
            

        }
        
        IEnumerator ShootLeft2()
        {
            //bulletLoader.circleLeft.fillAmount = 0;
            Instantiate(heavyBullet, shootFromLeft);
            canShootLeft2 = false;
            //bulletLoader.LoadBullet();
            yield return new WaitForSeconds(rechargeTimer);
            
        }

     
    }
  
}
