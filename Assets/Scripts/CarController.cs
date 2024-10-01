using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum WheelDrive
{
    Front, Rear
}

[Serializable]
public struct Wheel
{
    public WheelCollider wheelCollider;
    public GameObject wheelMesh;
    public GameObject skidMarks;
    public ParticleSystem smokeParticle;
    public WheelDrive wheelDrive;
}
public enum ControlMode
{
    Keyboard, Mobile
}


public class CarController : MonoBehaviour
{
    [SerializeField] private List<Wheel> _wheels;
    [SerializeField] private float _maxAcceleration;
    [SerializeField] private float _brakeAcceleration;
    [SerializeField] private float _maxBrake;
    [SerializeField] private GameObject _canvas,_anotherCanvas;
    [SerializeField] private TextMeshProUGUI _speedTMP;

    private CarLights _carLights;

    private float _carMovement;
    private float _carSteering;

    private float _maxSteerAngle = 30f;
    private float _carSensitivity = 1.0f;
    private Rigidbody _carRB;

    private Vector3 _centreofMass;
    private bool _brake;

   public ControlMode controlType;
    void Start()
    {
        Time.timeScale = 1;
        _carRB = GetComponentInChildren<Rigidbody>();
        _carLights = GetComponent<CarLights>();
        _carRB.centerOfMass = _centreofMass;
        if (controlType == ControlMode.Mobile && !_canvas.activeInHierarchy )
            _canvas.SetActive(true);        
        else if (controlType == ControlMode.Keyboard && _canvas.activeInHierarchy)        
            _canvas.SetActive(false);

        if (_anotherCanvas == null) return;
        if (controlType == ControlMode.Keyboard && !_anotherCanvas.activeInHierarchy)
            _anotherCanvas.SetActive(true);
        else if(controlType== ControlMode.Mobile && _anotherCanvas.activeInHierarchy)
            _anotherCanvas.SetActive(false);
        
    }

    void Update()
    {
        CarInput();
        WheelAnimations();
       // CarInfo();
        
    }

    private void FixedUpdate()
    {
        CarMovement();
        CarSteering();
        CarBrake();
    }

    private void LateUpdate()
    {
        CarInfo();
    }



    public void MoveInput(float moveInput)
    {
        _carMovement = moveInput * _maxAcceleration * 300f * Time.deltaTime;
    }
    public void SteerInput(float steerInput)
    {
        _carSteering = steerInput * _maxSteerAngle * _carSensitivity;
    }

    public void BrakeInput(bool _isBraking)
    {
        _brake = _isBraking;
    }
    private void CarInput()
    {
        if (controlType == ControlMode.Keyboard)
        {
            _carMovement = Input.GetAxis("Vertical") * _maxAcceleration * 600f * Time.deltaTime;
            _carSteering = Input.GetAxis("Horizontal") * _maxSteerAngle * _carSensitivity;
        }
    }

    public void CarMovement()
    {
        foreach (Wheel wheel in _wheels)
        {
            wheel.wheelCollider.motorTorque = _carMovement;
        }
    }

    public void CarSteering()
    {
        foreach (Wheel wheel in _wheels)
        {
            if (wheel.wheelDrive == WheelDrive.Front)
            {
                float steerAngle = _carSteering;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, steerAngle, .6f);
            }
        }
    }

    public void CarBrake()
    {
        foreach (Wheel wheel in _wheels)
        {
            if ((Input.GetKey(KeyCode.Space) || _brake==true) && wheel.wheelDrive == WheelDrive.Rear)
            {
                wheel.wheelCollider.brakeTorque = _maxBrake * 300f * _brakeAcceleration * Time.fixedDeltaTime;
                wheel.skidMarks.GetComponent<TrailRenderer>().emitting = true;
                wheel.smokeParticle.Emit(1);
                _carLights.isBackLightOn = true;
                _carLights.BackLightOn();
            }
            else
            {
                wheel.wheelCollider.brakeTorque = 0;
                wheel.skidMarks.GetComponent<TrailRenderer>().emitting = false;
                _carLights.isBackLightOn = false;
                _carLights.BackLightOn();
            }
        }
    }

    public void WheelAnimations()
    {
        foreach (Wheel wheel in _wheels)
        {
            wheel.wheelCollider.GetWorldPose(out Vector3 wheelPos, out Quaternion wheelRot);
            wheel.wheelMesh.transform.SetPositionAndRotation(wheelPos, wheelRot);
        }
    }

    public void CarInfo()
    {
        //float speed= Mathf.Lerp(_carRB.velocity.magnitude, _maxAcceleration, .6f*Time.deltaTime);
        //_speedTMP.text =Mathf.FloorToInt(speed) + "Km/h";

        _speedTMP.text = Mathf.FloorToInt(_carRB.velocity.magnitude) + " Km/h";
    }
}
