using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DistanceActivator : MonoBehaviour
{
    [Tooltip("Abstand in Metern, ab dem das Objekt sichtbar wird.")]
    public float activationDistance = 50f;
    Renderer rend;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        if (rend != null) rend.enabled = false;
    }

    void Update()
    {
        if (!Camera.main) return;
        float d = Vector3.Distance(Camera.main.transform.position, transform.position);
        if (rend != null)
            rend.enabled = (d <= activationDistance);
    }
}
