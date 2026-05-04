using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI References")]
    public TextMeshProUGUI vialNameText; 

    private Vector2 originalPosition;
    private Vector2 textOriginalPosition;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null) 
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        originalPosition = rectTransform.anchoredPosition;

        if (vialNameText != null)
        {
            textOriginalPosition = vialNameText.rectTransform.anchoredPosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform.parent as RectTransform, 
            eventData.position, 
            eventData.pressEventCamera, 
            out globalMousePos
        );
        
        rectTransform.position = globalMousePos;

        if (vialNameText != null)
        {
            vialNameText.rectTransform.position = new Vector3(globalMousePos.x, globalMousePos.y - 50, globalMousePos.z);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        GameObject cauldronObj = GameObject.Find("Cauldron");

        if (cauldronObj != null)
        {
            float distance = Vector2.Distance(rectTransform.position, cauldronObj.transform.position);

            if (distance < 150f)
            {
                PlayerPrefs.SetInt("IsCauldronFull", 1);
                PlayerPrefs.Save();
                
                Debug.Log("Vial berhasil masuk ke kuali dan data dari PlayerPrefs dimuat.");

                ResetItem();
                return;
            }
        }

        // Kembalikan ke posisi semula jika tidak masuk ke kuali
        rectTransform.anchoredPosition = originalPosition;

        if (vialNameText != null)
        {
            vialNameText.rectTransform.anchoredPosition = textOriginalPosition;
        }
    }

    public void ResetItem()
    {
        gameObject.SetActive(false);
        if (vialNameText != null)
        {
            vialNameText.text = "";
            vialNameText.gameObject.SetActive(false);
        }
    }
}