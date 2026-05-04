using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Fungsi untuk pindah ke scene Data Collector
    public void GoToDataCollector()
    {
        SceneManager.LoadScene("DataCollector");
    }
// Fungsi untuk pindah ke scene Data Collector
    public void GoToBookShelf()
    {
        SceneManager.LoadScene("BookShelf");
    }

// Fungsi untuk pindah ke scene Data Collector
    public void GoToCrystal()
    {
        SceneManager.LoadScene("DataCrystal");
    }

    // Fungsi untuk pindah ke scene Cauldron
    public void GoToCauldron()
    {
        SceneManager.LoadScene("Cauldron");
    }
    
    // Fungsi untuk kembali ke Main Hub
    public void GoToMainHub()
    {
        SceneManager.LoadScene("MainHub");
    }

    // Fungsi untuk keluar dari game (opsional untuk menu utama)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game exited");
    }
}