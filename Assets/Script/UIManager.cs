using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject fragebogenPopup;
    public GameObject allgemeineInfoPopup;
    public GameObject tipOverlay;
    public GameObject[] infoPanels; // Liste für mehrere Gebäude

    [Header("External Links")]
    [Tooltip("URL zum Google Form")]
    public string fragebogenUrl;

    void Start()
    {
        // Tipp-Overlay einmalig beim Start anzeigen
        if (tipOverlay != null)
        {
            tipOverlay.SetActive(true);
        }
    }

    // BACK Button
    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("StartScene");
    }

    // Alles schließen
    public void CloseAllOverlays()
    {
        CloseFragebogenPopup();
        CloseAllgemeineInfo();
        CloseTipOverlay();
        HideAllPanels();
    }

    // Tipp-Overlay steuern
    public void ShowTipOverlay()
    {
        CloseAllOverlays();
        if (tipOverlay != null)
            tipOverlay.SetActive(true);
    }

    public void CloseTipOverlay()
    {
        if (tipOverlay != null)
            tipOverlay.SetActive(false);
    }

    // Fragebogen: Popup öffnen
    public void ShowFragebogenPopup()
    {
        CloseAllOverlays();
        if (fragebogenPopup != null)
            fragebogenPopup.SetActive(true);
    }

    public void CloseFragebogenPopup()
    {
        if (fragebogenPopup != null)
            fragebogenPopup.SetActive(false);
    }

    // Fragebogen starten (öffnet externen Link)
    public void OnFragebogenStart()
    {
        if (!string.IsNullOrEmpty(fragebogenUrl))
        {
            UnityEngine.Application.OpenURL(fragebogenUrl);
        }
    }

    // Allgemeine Info
    public void ToggleAllgemeineInfo()
    {
        CloseAllOverlays();
        if (allgemeineInfoPopup != null)
            allgemeineInfoPopup.SetActive(true);
    }

    public void CloseAllgemeineInfo()
    {
        if (allgemeineInfoPopup != null)
            allgemeineInfoPopup.SetActive(false);
    }

    // Panels für Gebäude
    public void ShowPanel(int index)
    {
        CloseAllOverlays();
        if (index >= 0 && index < infoPanels.Length)
            infoPanels[index].SetActive(true);
    }

    public void HideAllPanels()
    {
        if (infoPanels == null) return;
        foreach (var panel in infoPanels)
            panel.SetActive(false);
    }
}
