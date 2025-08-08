using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Google.XR.ARCoreExtensions;
using UnityEngine.XR.ARFoundation;


public class StatusDisplay : MonoBehaviour
{
    [SerializeField] private AREarthManager earthManager;
    [SerializeField] private TextMeshProUGUI statusText;

    private VpsAvailabilityPromise vpsPromise;
    private string vpsStatus = "VPS: noch nicht geprüft";

    void Update()
    {
        // 1) GPS verfügbar?
        bool gpsRunning = Input.location.status == LocationServiceStatus.Running;

        // 2) VPS-Verfügbarkeit prüfen (einmalig)
        var pose = earthManager.CameraGeospatialPose;
        if (vpsPromise == null && pose.OrientationYawAccuracy <= 25f)
        {
            vpsPromise = AREarthManager.CheckVpsAvailabilityAsync(
                pose.Latitude, pose.Longitude);
        }
        if (vpsPromise != null && vpsPromise.State == PromiseState.Done)
        {
            vpsStatus = vpsPromise.Result == VpsAvailability.Available
                ? "VPS: verfügbar" : "VPS: NICHT verfügbar";
            vpsPromise = null;
        }

        // 3) Tracking-State
        string tracking = ARSession.state == ARSessionState.SessionTracking
            ? "AR Tracking: aktiv" : "AR Tracking: nicht aktiv";

        // Zeile z. B.:
        statusText.text =
            $"GPS: {(gpsRunning ? "läuft" : "aus")}\n" +
            $"{vpsStatus}\n" +
            $"{tracking}\n" +
            $"YawGenauigkeit: {pose.OrientationYawAccuracy:F1}°";
    }
}