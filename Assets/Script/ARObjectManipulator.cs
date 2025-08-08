using UnityEngine;
using UnityEngine.EventSystems;

public class ARObjectManipulator : MonoBehaviour
{
    private Camera arCamera;
    private Plane dragPlane;
    private Vector2 prevTouchPos1, prevTouchPos2;
    private bool isDragging = false;

    void Start()
    {
        arCamera = Camera.main;
        dragPlane = new Plane(Vector3.up, transform.position);
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = arCamera.ScreenPointToRay(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
                {
                    isDragging = true;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                if (dragPlane.Raycast(ray, out float enter))
                {
                    Vector3 hitPoint = ray.GetPoint(enter);
                    transform.position = hitPoint;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 currTouch0 = touch0.position;
            Vector2 currTouch1 = touch1.position;
            Vector2 prevTouch0 = touch0.position - touch0.deltaPosition;
            Vector2 prevTouch1 = touch1.position - touch1.deltaPosition;

            float prevDistance = Vector2.Distance(prevTouch0, prevTouch1);
            float currDistance = Vector2.Distance(currTouch0, currTouch1);

            float scaleFactor = 1 + (currDistance - prevDistance) * 0.002f;
            transform.localScale *= scaleFactor;

            float rotationAngle = Vector2.SignedAngle(prevTouch1 - prevTouch0, currTouch1 - currTouch0);
            transform.Rotate(Vector3.up, rotationAngle, Space.World);
        }
    }
}