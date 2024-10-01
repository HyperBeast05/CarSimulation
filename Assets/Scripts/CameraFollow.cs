using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _targetCar;
    [SerializeField] private float _movementSensitivity;
    [SerializeField] private float _rotationSensitivity;
    [SerializeField] private Vector3 _moveOffset;
    [SerializeField] private Vector3 _rotationOffset;
    private void FixedUpdate()
    {
        FollowTarget();   
    }

    private void CameraMovement()
    {
        Vector3 targetPos = _targetCar.TransformPoint(_moveOffset);
        transform.position = Vector3.Lerp(transform.position, targetPos, _movementSensitivity * Time.fixedDeltaTime);
    }

    private void CameraRotation()
    {
        Vector3 direction = _targetCar.position-transform.position;
        Quaternion targetRot = Quaternion.LookRotation(direction+_rotationOffset, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation,targetRot,_rotationSensitivity*Time.fixedDeltaTime);
    }

    private void FollowTarget()
    {
        CameraMovement();
        CameraRotation();
    }
}
