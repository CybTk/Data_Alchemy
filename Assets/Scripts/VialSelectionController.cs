using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class VialSelectionController : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject popupPanel;
    public TextMeshProUGUI popupTitle;
    public TextMeshProUGUI popupNarrative;

    [Header("Vial Data References")]
    public VialData vial1Data;
    public VialData vial2Data;

    private VialData selectedVial;

    // 1. Tambahkan instance agar bisa dipanggil dari skrip lain
    public static VialSelectionController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Agar tetap ada saat pindah scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }

    public void OpenPopupForVial1()
    {
        selectedVial = vial1Data;
        ShowPopup();
    }

    public void OpenPopupForVial2()
    {
        selectedVial = vial2Data;
        ShowPopup();
    }

    private void ShowPopup()
    {
        if (selectedVial != null)
        {
            if (popupPanel != null) popupPanel.SetActive(true);
            if (popupTitle != null) popupTitle.text = selectedVial.vialName;
            if (popupNarrative != null) popupNarrative.text = selectedVial.narrativeText;
        }
    }

    public void ClosePopup()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }

    public void TakeVialAndReturnToLobby()
    {
        if (selectedVial != null)
        {
            PlayerPrefs.SetString("VialName", selectedVial.vialName);
            PlayerPrefs.SetString("CurrentRawData", selectedVial.rawDataContent);
            PlayerPrefs.SetString("CurrentNarrative", selectedVial.narrativeText);
            PlayerPrefs.SetInt("IsCauldronFull", 0); 
            PlayerPrefs.SetInt("HasVial", 1);
            PlayerPrefs.Save();
            
            Debug.Log("Vial berhasil dipilih dan disimpan: " + selectedVial.vialName);
            SceneManager.LoadScene("MainHub");
        }
        else
        {
            Debug.LogWarning("Tidak ada vial yang dipilih!");
        }
    }
}