using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CauldronInventory : MonoBehaviour
{
    [Header("UI Inventory - Cauldron Scene")]
    public GameObject inventoryPanel;
    public TextMeshProUGUI vialNameText;
    public Image vialIcon;

    void Start()
    {
        // Selalu tampilkan bar inventory di layar scene Cauldron
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(true);
        }

        // Membaca data yang dibawa dari PlayerPrefs tanpa mengganggu Main Hub
        if (PlayerPrefs.GetInt("HasVial", 0) == 1)
        {
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
                vialNameText.text = "";
            }

            if (vialIcon != null)
            {
                vialIcon.gameObject.SetActive(false);
            }
        }
    }
}