using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("UI Inventory")]
    public GameObject inventoryPanel;
    public TextMeshProUGUI vialNameText; // Menggunakan nama variabel yang konsisten
    public Image vialIcon;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Menjaga inventory tetap ada saat pindah scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateInventoryDisplay();
    }

    public void UpdateInventoryDisplay()
    {
        if (PlayerPrefs.GetInt("HasVial", 0) == 1)
        {
            if (inventoryPanel != null) inventoryPanel.SetActive(true);
            
            if (vialNameText != null)
            {
                vialNameText.text = PlayerPrefs.GetString("VialName", "Vial");
            }

            if (vialIcon != null)
            {
                vialIcon.gameObject.SetActive(true);
            }
        }
        else
        {
            if (vialNameText != null)
            {
                vialNameText.text = ""; // Dikosongkan saat tidak ada item
            }
            
            if (vialIcon != null)
            {
                vialIcon.gameObject.SetActive(false);
            }
        }
    }

    public void ClearInventory()
    {
        PlayerPrefs.DeleteKey("HasVial");
        PlayerPrefs.DeleteKey("VialName");
        PlayerPrefs.DeleteKey("RawData");
        PlayerPrefs.DeleteKey("Narrative");
        PlayerPrefs.Save();
        UpdateInventoryDisplay();
    }
}