using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class GarageImageSpawner : MonoBehaviour
{
    [SerializeField] GameObject garagePrefab;             // Für Image Tracking
    [SerializeField] GameObject geoAnchorGarageObject;    // Für Geospatial Anchor

    private ARTrackedImageManager imgManager;
    private GameObject spawnedGarage;

    void Awake()
    {
        imgManager = GetComponent<ARTrackedImageManager>();

        if (geoAnchorGarageObject != null) geoAnchorGarageObject.SetActive(false);
    }

    void OnEnable() => imgManager.trackedImagesChanged += OnChanged;
    void OnDisable() => imgManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs args)
    {
        // Bild erkannt
        foreach (var tracked in args.added)
        {
            // Deaktiviere Geo-Anker (falls aktiv)
            if (geoAnchorGarageObject != null)
                geoAnchorGarageObject.SetActive(false);

            // Spawn GarageContent am Marker
            if (garagePrefab != null && spawnedGarage == null)
            {
                spawnedGarage = Instantiate(garagePrefab, tracked.transform);
                spawnedGarage.transform.localPosition = Vector3.zero;
                spawnedGarage.transform.localRotation = Quaternion.identity;
            }
        }

        // Bild verloren → zurück zu Geo-Anker
        foreach (var tracked in args.removed)
        {
            if (spawnedGarage != null)
            {
                Destroy(spawnedGarage);
                spawnedGarage = null;
            }

            if (geoAnchorGarageObject != null)
                geoAnchorGarageObject.SetActive(true);
        }
    }
}
