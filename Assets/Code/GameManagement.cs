using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // U¿ywamy TextMeshPro
using System.Collections;

public class GameManagement : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject levelCompletedPanel; // Panel z napisem "Level Completed"
    [SerializeField] private GameObject blockerPanel; // Panel blokuj¹cy interakcje (czarny ekran)

    private TextMeshProUGUI levelCompletedText; // Komponent TextMeshProUGUI w panelu levelCompletedPanel

    private void Start()
    {
        blockerPanel.SetActive(false); // Na pocz¹tku ukrywamy blockerPanel
        levelCompletedPanel.SetActive(false); // Na pocz¹tku ukrywamy levelCompletedPanel

        // Pobieramy komponent TextMeshProUGUI z panelu levelCompletedPanel
        levelCompletedText = levelCompletedPanel.GetComponentInChildren<TextMeshProUGUI>();

        // Debugowanie - sprawdŸ, czy znaleziono komponent TextMeshProUGUI
        if (levelCompletedText == null)
        {
            Debug.LogError("Nie znaleziono komponentu TextMeshProUGUI w dzieciach levelCompletedPanel!");
        }
        else
        {
            Debug.Log("Komponent TextMeshProUGUI znaleziony: " + levelCompletedText.name);
        }
    }

    // Metoda do wyœwietlania komunikatu o zakoñczeniu poziomu
    public void ShowLevelCompleted()
    {
        blockerPanel.SetActive(true); // Pokazuje czarny ekran
        levelCompletedPanel.SetActive(true); // Pokazuje panel z napisem "Level Completed"

        // Ustawienie tekstu w zale¿noœci od sytuacji
        if (levelCompletedText != null)
        {
            levelCompletedText.text = "Level Completed!"; // Ustawia tekst w panelu
        }

        StartCoroutine(LoadNextLevelAfterDelay(3f)); // Po 3 sekundach ³aduje nastêpny poziom
    }

    // Metoda do wyœwietlania komunikatu o Game Over
    public void ShowGameOver()
    {
        blockerPanel.SetActive(true); // Pokazuje czarny ekran
        levelCompletedPanel.SetActive(true); // Pokazuje panel z napisem "Game Over"

        // Ustawienie tekstu w zale¿noœci od sytuacji
        if (levelCompletedText != null)
        {
            levelCompletedText.text = "Game Over!"; // Ustawia tekst w panelu
        }

        StartCoroutine(LoadMainMenuAfterDelay(3f)); // Po 3 sekundach ³aduje g³ówne menu
    }

    // Przejœcie do nastêpnego poziomu
    private IEnumerator LoadNextLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Czeka 3 sekundy

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Pobiera indeks obecnej sceny
        int nextSceneIndex = currentSceneIndex + 1; // Oblicza indeks nastêpnej sceny

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) // Sprawdza, czy istnieje nastêpna scena
        {
            SceneManager.LoadScene(nextSceneIndex); // £aduje nastêpn¹ scenê
        }
        else
        {
            Debug.Log("Brak nastêpnego poziomu!"); // Jeœli nie ma nastêpnego poziomu
        }
    }

    // Powrót do g³ównego menu po Game Over
    private IEnumerator LoadMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Czeka 3 sekundy
        SceneManager.LoadScene("MainMenu"); // £aduje scenê g³ównego menu (upewnij siê, ¿e masz odpowiedni¹ nazwê sceny)
    }
}
