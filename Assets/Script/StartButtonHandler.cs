using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonHandler : MonoBehaviour
{
    public GameObject permissionPopup;
    public GameObject startScreenUI; // Neu hinzugefügt

    public void OnStartButtonClicked()
    {
        startScreenUI.SetActive(true);      // Startscreen ausblenden
        permissionPopup.SetActive(true);     // Popup anzeigen
    }

    public void OnPermissionAccepted()
    {
        Debug.Log("Zugriff erlaubt – weiter zur AR Szene...");
        SceneManager.LoadScene("ARScene");

    }

    public void OnPermissionDeclined()
    {
        permissionPopup.SetActive(false);    // Popup schließen
        startScreenUI.SetActive(true);       // Startscreen wieder anzeigen
        Debug.Log("Zugriff abgelehnt – zurück zum Startscreen");
    }
}
