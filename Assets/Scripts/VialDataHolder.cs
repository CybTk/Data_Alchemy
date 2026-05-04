using UnityEngine;

public class VialDataHolder : MonoBehaviour
{
    public VialData vial1Data;
    public VialData vial2Data;

    public static VialDataHolder instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public VialData GetVialDataByName(string vialName)
    {
        Debug.Log("Mencari data vial dengan nama: " + vialName);

        // Cek spesifik Vial 1
        if (vial1Data != null && (vial1Data.vialName == vialName || vial1Data.name == vialName))
        {
            return vial1Data;
        }

        // Cek spesifik Vial 2
        if (vial2Data != null && (vial2Data.vialName == vialName || vial2Data.name == vialName))
        {
            return vial2Data;
        }

        Debug.LogWarning("VialData tidak ditemukan untuk: " + vialName);
        return null;
    }
}