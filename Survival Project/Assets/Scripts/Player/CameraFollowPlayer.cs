using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _targetToFollow;
    [SerializeField] private Vector3 _cameraOffset;
    private float _cameraSmoothSpeed = 0.1f;

    void Start()
    {
        _targetToFollow = GameObject.Find("Player").transform;
    }
    void FixedUpdate()
    {
        Vector3 desiredPosition = _targetToFollow.position + _cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _cameraSmoothSpeed);
        transform.position = smoothedPosition;
    }
}
