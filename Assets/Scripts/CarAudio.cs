
using UnityEngine;

public class CarAudio : MonoBehaviour
{
    [SerializeField] private float _minSpeed, _maxSpeed, _currentSpeed;
    [SerializeField] private float _minPitch, _maxPitch;
    [SerializeField] private float _pitchFromTheCar;

    private AudioSource _carAudio;
    private Rigidbody _carRB;

    void Start()
    {
        _carAudio = GetComponent<AudioSource>();
        _carRB  =GetComponent<Rigidbody>();
    }

    void Update()
    {
        CarEngineSound();
    }

    private void CarEngineSound()
    {
        _currentSpeed = _carRB.velocity.magnitude;
        _pitchFromTheCar = _currentSpeed /_maxSpeed;

        if (_currentSpeed < _minSpeed)
            _carAudio.pitch = _minPitch;

        if (_currentSpeed > _minSpeed && _currentSpeed < _maxSpeed)
            _carAudio.pitch = _minPitch + _pitchFromTheCar;

        if (_currentSpeed > _maxSpeed)
            _carAudio.pitch = _maxPitch;

    }

}
