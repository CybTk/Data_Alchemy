using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainHubController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public TextMeshProUGUI vialNameText;
    public Image vialIcon; 

    void Awake()
    {
        // Hapus data lama saat game baru dimulai agar selalu fresh
        if (Time.timeSinceLevelLoad < 1f)
        {
            PlayerPrefs.DeleteKey("HasVial");
            PlayerPrefs.DeleteKey("VialName");
            PlayerPrefs.DeleteKey("RawData");
            PlayerPrefs.DeleteKey("Narrative");
            PlayerPrefs.Save();
        }
    }

    void Start()
    {
        // Pastikan panel aktif saat game dimulai
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(true);
        }

        // Cek apakah pemain membawa vial atau belum
        if (PlayerPrefs.GetInt("HasVial", 0) == 1)
        {
            // Jika ada vial, tampilkan nama dan ikon
            if (vialNameText != null)
            {
                vialNameText.text = PlayerPrefs.GetString("VialName", "Vial");
            }
            
            if (vialIcon != null)
            {
                vialIcon.gameObject.SetActive(true); // Menampilkan ikon
            }
        }
        else
        {
            // Jika kosong, ubah teks menjadi string kosong ("")
            if (vialNameText != null)
            {
                vialNameText.text = ""; 
            }

            if (vialIcon != null)
            {
                vialIcon.gameObject.SetActive(false); // Menyembunyikan ikon
            }
        }
    }
}