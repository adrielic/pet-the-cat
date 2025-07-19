using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [Header("Pan Settings")]
    public float panSpeed = 0.01f;
    public Vector2 panLimitMin = new Vector2(-20f, -20f);
    public Vector2 panLimitMax = new Vector2(20f, 20f);
    public float panSmoothTime = 0.1f;

    [Header("Zoom Settings")]
    public float zoomSpeedTouch = 0.1f;
    public float zoomSpeedMouse = 5f;
    public float minZoom = 3f;
    public float maxZoom = 10f;
    public float zoomSmoothTime = 0.1f;

    private Vector3 targetPosition;
    private float targetZoom;
    private Vector3 velocity = Vector3.zero;
    private float zoomVelocity = 0f;
    private Vector3 lastMousePanPosition;
    private Vector2 lastTouchPanPosition;
    private int panFingerId;
    private bool isPanning;
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
        targetPosition = cam.transform.position;
        targetZoom = cam.orthographicSize;
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        HandleMouse();
#else
        HandleTouch();
#endif

        SmoothMovement();
    }

    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePanPosition = Input.mousePosition;
            isPanning = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isPanning = false;
        }

        if (isPanning)
        {
            Vector3 delta = Input.mousePosition - lastMousePanPosition;
            PanCamera(delta);
            lastMousePanPosition = Input.mousePosition;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll * zoomSpeedMouse);
    }

    void HandleTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPanPosition = touch.position;
                panFingerId = touch.fingerId;
                isPanning = true;
            }
            else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.position - lastTouchPanPosition;
                PanCamera(new Vector3(delta.x, delta.y, 0f));
                lastTouchPanPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isPanning = false;
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            float prevDistance = (t0.position - t0.deltaPosition - (t1.position - t1.deltaPosition)).magnitude;
            float currDistance = (t0.position - t1.position).magnitude;

            float delta = currDistance - prevDistance;
            ZoomCamera(delta * zoomSpeedTouch);
        }
    }

    void PanCamera(Vector3 delta)
    {
        Vector3 move = new Vector3(-delta.x * panSpeed, -delta.y * panSpeed, 0f);
        targetPosition += move;

        // Clampear os limites
        targetPosition.x = Mathf.Clamp(targetPosition.x, panLimitMin.x, panLimitMax.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, panLimitMin.y, panLimitMax.y);
    }

    void ZoomCamera(float delta)
    {
        targetZoom = Mathf.Clamp(targetZoom - delta, minZoom, maxZoom);
    }

    void SmoothMovement()
    {
        // Move suavemente a posição
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, targetPosition, ref velocity, panSmoothTime);

        // Faz o zoom suavemente
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoom, ref zoomVelocity, zoomSmoothTime);
    }
}
