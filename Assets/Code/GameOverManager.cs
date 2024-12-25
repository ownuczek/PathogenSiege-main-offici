using UnityEngine;  // Upewnij siê, ¿e ta linia jest dodana
using TMPro;  // Dodaj przestrzeñ nazw TextMeshPro, jeœli u¿ywasz TextMeshPro
using UnityEngine.SceneManagement;  // Dodaj przestrzeñ nazw dla SceneManager


public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel; // Panel Game Over
    [SerializeField] private GameObject blockerPanel; // Panel blokuj¹cy interakcje
    [SerializeField] private TextMeshProUGUI gameOverText; // Komponent TextMeshProUGUI

    private void Start()
    {
        gameOverPanel.SetActive(false); // Ukrywamy ekran Game Over
        blockerPanel.SetActive(false); // Ukrywamy panel blokuj¹cy interakcje
    }

    // Metoda do pokazania ekranu Game Over
    public void ShowGameOver()
    {
        blockerPanel.SetActive(true); // Pokazujemy czarny ekran blokuj¹cy
        gameOverPanel.SetActive(true); // Pokazujemy ekran Game Over

        // Ustawienie tekstu "Game Over" w przypadku przegranej
        if (gameOverText != null)
        {
            gameOverText.text = "Game Over!"; // Ustawiamy komunikat
        }
    }

    // Funkcja do restartu gry
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // £aduje obecn¹ scenê (restartuje)
    }

    // Funkcja do przejœcia do menu g³ównego
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu"); // Zmieniamy "MainMenu" na nazwê sceny menu
    }
}
