using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [Header("Target")]
    public Transform target;

    [Header("Bounds")]
    public Vector2 minBounds;
    public Vector2 maxBounds;

    [Header("Offset")]
    public Vector3 offset;

    [Header("Camera Settings")]
    public float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPos = target.position + offset;
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Clamp position inside bounds
        float clampedX = Mathf.Clamp(smoothPos.x, minBounds.x + camWidth, maxBounds.x - camWidth);
        float clampedY = Mathf.Clamp(smoothPos.y, minBounds.y + camHeight, maxBounds.y - camHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Camera cam = Camera.main;
        float camHeight = cam != null ? cam.orthographicSize : 5f;
        float camWidth = cam != null ? camHeight * cam.aspect : 5f * 1.777f;

        Vector3 bottomLeft = new Vector3(minBounds.x + camWidth, minBounds.y + camHeight, 0f);
        Vector3 topRight = new Vector3(maxBounds.x - camWidth, maxBounds.y - camHeight, 0f);
        Vector3 size = topRight - bottomLeft;

        Gizmos.DrawWireCube(bottomLeft + size / 2f, size);
    }

}
