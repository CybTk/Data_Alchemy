using UnityEngine;
using UnityEngine.EventSystems;

public class PotionInteraction : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 originalPos;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPos = transform.position;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.7f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        // Periksa apakah di-drop di atas Cauldron
        if (eventData.pointerEnter != null && eventData.pointerEnter.name == "Cauldron")
        {
            ProcessPotionEffect();
        }
        else
        {
            transform.position = originalPos; // Kembali ke meja
        }
    }

    private void ProcessPotionEffect()
    {
        // Pastikan kuali sudah ada isinya
        if (PlayerPrefs.GetInt("IsCauldronFull", 0) == 0)
        {
            Debug.Log("Kuali masih kosong, ramuan tidak bisa digunakan.");
            return;
        }

        string rawData = PlayerPrefs.GetString("CurrentRawData", "");
        string narrative = PlayerPrefs.GetString("CurrentNarrative", "");

        if (gameObject.name.Contains("NullPurge") && DataCleaner.instance != null)
        {
            string cleanedData = DataCleaner.instance.ExecuteNullPurge(rawData);
            PlayerPrefs.SetString("CurrentRawData", cleanedData);
            PlayerPrefs.SetString("CurrentNarrative", narrative + "\n[Null Purge diaplikasikan]");
            PlayerPrefs.Save();
            
            Debug.Log("Ramuan Null Purge dituangkan!");
            Destroy(gameObject); // Hilangkan botol ramuan
        }
        else if (gameObject.name.Contains("Duplicate") && DataCleaner.instance != null)
        {
            string cleanedData = DataCleaner.instance.ExecuteDuplicateDissolver(rawData);
            PlayerPrefs.SetString("CurrentRawData", cleanedData);
            PlayerPrefs.SetString("CurrentNarrative", narrative + "\n[Duplicate Dissolver diaplikasikan]");
            PlayerPrefs.Save();
            
            Debug.Log("Ramuan Duplicate Dissolver dituangkan!");
            Destroy(gameObject); // Hilangkan botol ramuan
        }
    }
}