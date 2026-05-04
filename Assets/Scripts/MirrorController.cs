using UnityEngine;
using TMPro;

public class MirrorController : MonoBehaviour
{
    [Header("Mirror UI Panel")]
    public GameObject mirrorPanel;
    public TextMeshProUGUI rawDataText;
    public TextMeshProUGUI narrativeText;

    void Start()
    {
        if (mirrorPanel != null)
            mirrorPanel.SetActive(false);
    }

    public void OpenMirror()
    {
        if (mirrorPanel != null)
            mirrorPanel.SetActive(true);

        // Membaca dari memori sementara secara langsung
        if (PlayerPrefs.GetInt("IsCauldronFull", 0) == 1)
        {
            if (rawDataText != null)
                rawDataText.text = PlayerPrefs.GetString("CurrentRawData", "Data Kosong");

            if (narrativeText != null)
                narrativeText.text = PlayerPrefs.GetString("CurrentNarrative", "Narasi tidak ditemukan");
        }
        else
        {
            if (rawDataText != null) rawDataText.text = "Kuali belum terisi.";
            if (narrativeText != null) narrativeText.text = "Kuali belum terisi.";
        }
    }

    public void CloseMirror()
    {
        if (mirrorPanel != null)
            mirrorPanel.SetActive(false);
    }
}