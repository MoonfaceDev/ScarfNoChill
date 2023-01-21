using UnityEngine;

public class CameraMovement : BaseComponent
{
    public Rect worldBorder;
    public Vector3 targetPosition;
    public float lerpSpeed = 3.0f;

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
        var next = Vector3.Lerp(position, targetPosition, lerpSpeed * Time.deltaTime);
        if ((next.x - CameraWidth / 2 > worldBorder.xMin && next.x + CameraWidth / 2 < worldBorder.xMax) ||
            (position.x < next.x && position.x - CameraWidth / 2 < worldBorder.xMin) ||
            (position.x > next.x && position.x + CameraWidth / 2 > worldBorder.xMax))
        {
            position += (next.x - position.x) * Vector3.right;
        }

        if ((next.y - CameraHeight / 2 > worldBorder.yMin && next.y + CameraHeight / 2 < worldBorder.yMax) ||
            (position.y < next.y && position.y - CameraHeight / 2 < worldBorder.yMin) ||
            (position.y > next.y && position.y + CameraHeight / 2 > worldBorder.yMax))
        {
            position += (next.y - position.y) * Vector3.up;
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