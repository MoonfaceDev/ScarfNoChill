using UnityEngine;

[RequireComponent(typeof(CameraMovement))]
public class CameraFollow : BaseComponent
{
    public Transform target;
    
    private CameraMovement cameraMovement;
    private Vector3 offset;

    private void Awake()
    {
        cameraMovement = GetComponent<CameraMovement>();
    }

    private void Start()
    {
        offset = transform.position - target.position;
        offset.x = 0;
    }

    private void LateUpdate()
    {
        var targetPosition = target.position + offset;
        cameraMovement.targetPosition = targetPosition;
    }
}