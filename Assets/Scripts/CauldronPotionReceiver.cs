using UnityEngine;
using UnityEngine.EventSystems;

public class CauldronPotionReceiver : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // Pastikan ada objek yang di-drop dan merupakan Potion
        if (eventData.pointerDrag != null)
        {
            GameObject droppedObject = eventData.pointerDrag;

            // Periksa apakah objek yang di-drop adalah Potion
            if (droppedObject.name.Contains("Potion"))
            {
                Debug.Log("Potion berhasil di-drop ke Cauldron!");
                ProcessPotion(droppedObject);
            }
        }
    }

    private void ProcessPotion(GameObject potionObj)
    {
        // Pastikan kuali sudah terisi vial
        if (PlayerPrefs.GetInt("IsCauldronFull", 0) == 0)
        {
            Debug.Log("Kuali masih kosong, tidak bisa menggunakan ramuan.");
            return;
        }

        // Ambil data mentah dari PlayerPrefs
        string rawData = PlayerPrefs.GetString("CurrentRawData", "");
        string narrative = PlayerPrefs.GetString("CurrentNarrative", "");

        // Pengecekan Null Purge
        if (potionObj.name.Contains("NullPurge") && DataCleaner.instance != null)
        {
            string cleanedData = DataCleaner.instance.ExecuteNullPurge(rawData);
            
            PlayerPrefs.SetString("CurrentRawData", cleanedData);
            PlayerPrefs.SetString("CurrentNarrative", narrative + "\n[Null Purge diaplikasikan]");
            PlayerPrefs.Save();
            
            Debug.Log("Ramuan Null Purge dituangkan, data telah dibersihkan.");
            Destroy(potionObj);
        }
        // Pengecekan Duplicate Dissolver (Diperbarui agar lebih spesifik)
        else if ((potionObj.name.Contains("Duplicate") || potionObj.name.Contains("Dissolver")) && DataCleaner.instance != null)
        {
            // Panggil fungsi dari DataCleaner
            string cleanedData = DataCleaner.instance.ExecuteDuplicateDissolver(rawData);
            
            // Simpan kembali
            PlayerPrefs.SetString("CurrentRawData", cleanedData);
            PlayerPrefs.SetString("CurrentNarrative", narrative + "\n[Duplicate Dissolver diaplikasikan]");
            PlayerPrefs.Save();
            
            Debug.Log("Ramuan Duplicate Dissolver dituangkan, data telah diproses: " + cleanedData);
            Destroy(potionObj);
        }
        else
        {
            Debug.LogWarning("Skrip DataCleaner belum terpasang atau jenis ramuan tidak dikenali.");
        }
    }
}