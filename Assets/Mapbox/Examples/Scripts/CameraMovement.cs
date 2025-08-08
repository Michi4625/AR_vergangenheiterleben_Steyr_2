using UnityEngine;
using Mapbox.Unity.Map;

namespace Mapbox.Examples
{
    /// <summary>Pan + Pinch-Zoom nur für Touch-Geräte.</summary>
    [RequireComponent(typeof(Camera))]
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] AbstractMap _map;              // deine Map-Instanz
        [SerializeField] float _panSpeed = 0.7f;      // Finger-Weg → Meter-Faktor
        [SerializeField] float _zoomSpeed = 0.01f;     // Pinch-Delta → Size-Faktor
        [SerializeField] float _minSize = 50f;       // OrthographicSize-Grenzen
        [SerializeField] float _maxSize = 800f;

        Camera _cam;
        Vector2 _lastPanPos;

        void Awake()
        {
            _cam = GetComponent<Camera>();
            if (!_map) _map = FindObjectOfType<AbstractMap>();
        }

        void Update()
        {
            if (Input.touchCount == 1)                 // ===== 1-Finger-PAN =====
            {
                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                    _lastPanPos = touch.position;

                if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 delta = touch.position - _lastPanPos;
                    _lastPanPos = touch.position;

                    // nach rechts wischen → Karte nach links verschieben (und umgekehrt)
                    Vector3 move = new Vector3(-delta.x, 0, -delta.y) * _panSpeed * Time.deltaTime;
                    _cam.transform.Translate(move, Space.Self);
                }
            }
            else if (Input.touchCount == 2)            // ===== 2-Finger-ZOOM =====
            {
                var t0 = Input.GetTouch(0);
                var t1 = Input.GetTouch(1);

                float prevMag = (t0.position - t0.deltaPosition - (t1.position - t1.deltaPosition)).magnitude;
                float curMag = (t0.position - t1.position).magnitude;
                float diff = (prevMag - curMag) * _zoomSpeed;

                _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize + diff, _minSize, _maxSize);
            }
        }
    }
}
