using UnityEngine;

[CreateAssetMenu(fileName = "NewVial", menuName = "Vial Data")]
public class VialData : ScriptableObject
{
    public string vialName;
    [TextArea(3, 10)]
    public string narrativeText;
    
    // Data mentah (kotor) dalam bentuk string agar mudah ditampilkan di UI
    [TextArea(5, 20)]
    public string rawDataContent;
}