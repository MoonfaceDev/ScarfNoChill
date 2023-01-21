using UnityEngine;

public class CameraMovement : BaseComponent
{
    public Rect worldBorder;
    public Vector3 targetPosition;

    private Camera mainCamera;

    public float CameraHeight => 2f * mainCamera.orthographicSize;

    public float CameraWidth => CameraHeight * mainCamera.aspect;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void UpdatePosition()
    {
        var position = transform.position;
        if ((targetPosition.x - CameraWidth / 2 > worldBorder.xMin && targetPosition.x + CameraWidth / 2 < worldBorder.xMax) ||
            (position.x < targetPosition.x && position.x - CameraWidth / 2 < worldBorder.xMin) ||
            (position.x > targetPosition.x && position.x + CameraWidth / 2 > worldBorder.xMax))
        {
            position += (targetPosition.x - position.x) * Vector3.right;
        }

        if ((targetPosition.y - CameraHeight / 2 > worldBorder.yMin && targetPosition.y + CameraHeight / 2 < worldBorder.yMax) ||
            (position.y < targetPosition.y && position.y - CameraHeight / 2 < worldBorder.yMin) ||
            (position.y > targetPosition.y && position.y + CameraHeight / 2 > worldBorder.yMax))
        {
            position += (targetPosition.y - position.y) * Vector3.up;
        }

        transform.position = position;
    }

    private void LateUpdate()
    {
        UpdatePosition();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(worldBorder.center.x, worldBorder.center.y, 0.01f),
            new Vector3(worldBorder.size.x, worldBorder.size.y, 0.01f));
    }
}