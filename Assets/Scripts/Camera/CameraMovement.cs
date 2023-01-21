using UnityEngine;

public class CameraMovement : BaseComponent
{
    public Vector3 targetPosition;

    private void UpdatePosition()
    {
        transform.position = targetPosition;
    }

    private void LateUpdate()
    {
        UpdatePosition();
    }
}