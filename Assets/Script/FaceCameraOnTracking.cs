using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCameraOnTracking : MonoBehaviour
{
    private void Start()
    {
        // Kamera finden
        Camera arCamera = Camera.main;
        if (arCamera == null) return;

        // Richtung zur Kamera berechnen (horizontal)
        Vector3 direction = arCamera.transform.position - transform.position;
        direction.y = 0; // optional: Y ignorieren (nur horizontal ausrichten)

        transform.rotation = Quaternion.LookRotation(direction);
    }
}
