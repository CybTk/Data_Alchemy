using UnityEngine;
using UnityEngine.EventSystems;

public class PotionDropHandler : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject == null) return;

        // 1. Pengecekan: Jika yang di-drag adalah Vial, abaikan (biarkan diproses oleh skrip InventoryItemDrag)
        if (droppedObject.name.Contains("Vial") || droppedObject.CompareTag("Vial"))
        {
            return;
        }

        // 2. Pengecekan: Jika kuali belum ada isinya
        if (PlayerPrefs.GetInt("IsCauldronFull", 0) == 0)
        {
            Debug.Log("Kuali masih kosong! Masukkan vial terlebih dahulu.");
            return;
        }

        // Ambil data yang tersimpan
        string rawData = PlayerPrefs.GetString("CurrentRawData", "");
        string narrative = PlayerPrefs.GetString("CurrentNarrative", "");

        // 3. Eksekusi pembersihan data berdasarkan nama potion
        if (droppedObject.name.Contains("NullPurge"))
        {
            if (DataCleaner.instance != null)
            {
                string cleanedData = DataCleaner.instance.ExecuteNullPurge(rawData);
                PlayerPrefs.SetString("CurrentRawData", cleanedData);
                PlayerPrefs.SetString("CurrentNarrative", narrative + "\n[Null Purge diaplikasikan]");
                PlayerPrefs.Save();

                Debug.Log("Null Purge berhasil dituangkan ke kuali!");
                Destroy(droppedObject); // Menghilangkan icon potion dari meja
            }
        }
        else if (droppedObject.name.Contains("Duplicate"))
        {
            if (DataCleaner.instance != null)
            {
                string cleanedData = DataCleaner.instance.ExecuteDuplicateDissolver(rawData);
                PlayerPrefs.SetString("CurrentRawData", cleanedData);
                PlayerPrefs.SetString("CurrentNarrative", narrative + "\n[Duplicate Dissolver diaplikasikan]");
                PlayerPrefs.Save();

                Debug.Log("Duplicate Dissolver berhasil dituangkan ke kuali!");
                Destroy(droppedObject); // Menghilangkan icon potion dari meja
            }
        }
    }
}