using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class MapMarkerPlacer : MonoBehaviour
{
    public AbstractMap map;       // Die Map (z. B. aus MapManager)
    public GameObject pinPrefab;  // Dein MapPin mit SpriteRenderer
    [SerializeField] Vector2d location;

    GameObject spawnedPin;

    void Start()
    {
        if (map != null && pinPrefab != null)
        {
            Vector3 pos = map.GeoToWorldPosition(location, true);
            spawnedPin = Instantiate(pinPrefab, pos, Quaternion.Euler(90, 0, 0), transform);

        }
    }

    void Update()
    {
        if (spawnedPin != null && map != null)
        {
            spawnedPin.transform.position = map.GeoToWorldPosition(location, true);
        }
    }
}
