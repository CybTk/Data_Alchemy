using UnityEngine;
using UnityEngine.EventSystems;

public class CauldronController : MonoBehaviour
{
    // Fungsi untuk menuangkan Potion Null Purge ke dalam Kuali
    public void PourNullPurgePotion()
    {
        if (PlayerPrefs.GetInt("IsCauldronFull", 0) == 1)
        {
            string rawData = PlayerPrefs.GetString("CurrentRawData", "");
            string narrative = PlayerPrefs.GetString("CurrentNarrative", "");

            // Bersihkan data menggunakan DataCleaner
            string cleanedData = DataCleaner.instance.ExecuteNullPurge(rawData);
            string newNarrative = narrative + "\n[Null Purge diaplikasikan]";

            // Simpan kembali data yang telah dibersihkan
            PlayerPrefs.SetString("CurrentRawData", cleanedData);
            PlayerPrefs.SetString("CurrentNarrative", newNarrative);
            PlayerPrefs.Save();

            Debug.Log("Null Purge dituangkan ke kuali!");
        }
        else
        {
            Debug.Log("Kuali masih kosong, tidak bisa diberi ramuan.");
        }
    }

    // Fungsi untuk menuangkan Potion Duplicate Dissolver ke dalam Kuali
    public void PourDuplicatePotion()
    {
        if (PlayerPrefs.GetInt("IsCauldronFull", 0) == 1)
        {
            string rawData = PlayerPrefs.GetString("CurrentRawData", "");
            string narrative = PlayerPrefs.GetString("CurrentNarrative", "");

            string cleanedData = DataCleaner.instance.ExecuteDuplicateDissolver(rawData);
            string newNarrative = narrative + "\n[Duplicate Dissolver diaplikasikan]";

            PlayerPrefs.SetString("CurrentRawData", cleanedData);
            PlayerPrefs.SetString("CurrentNarrative", newNarrative);
            PlayerPrefs.Save();

            Debug.Log("Duplicate Dissolver dituangkan ke kuali!");
        }
        else
        {
            Debug.Log("Kuali masih kosong, tidak bisa diberi ramuan.");
        }
    }
}