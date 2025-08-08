using UnityEngine;

public class MapToggle : MonoBehaviour
{
    public GameObject mapObject;           // z. B. „Map“ GameObject
    public GameObject mapCameraObject;     // z. B. „MapCamera“ GameObject
    public Camera arCamera;                // deine AR-Kamera
    public GameObject pinObject;

    public void ToggleMap()
    {
        bool isActive = !mapObject.activeSelf;

        mapObject.SetActive(isActive);
        mapCameraObject.SetActive(isActive);

        if (pinObject != null)
            pinObject.SetActive(isActive);

        if (arCamera != null)
            arCamera.enabled = !isActive;
    }
}
