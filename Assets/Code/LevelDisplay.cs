using UnityEngine;
using TMPro;
using System.Collections;

public class LevelDisplay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI levelText; // Przypisz TextMeshProUGUI w inspektorze

    private void Start()
    {
        UpdateLevelDisplay(LevelManager.main.currentLevel); // Zaktualizowanie numeru poziomu przy starcie sceny
        StartCoroutine(HideLevelTextAfterDelay(3f)); // Ukryj tekst po 3 sekundach
    }

    // Funkcja do aktualizacji tekstu UI z poziomem
    public void UpdateLevelDisplay(int level)
    {
        levelText.text = "Level " + level; // Wyœwietlenie numeru poziomu
    }

    // Coroutine do ukrywania tekstu po opóŸnieniu
    private IEnumerator HideLevelTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Czekaj przez zadany czas
        levelText.gameObject.SetActive(false); // Ukryj tekst
    }
}
