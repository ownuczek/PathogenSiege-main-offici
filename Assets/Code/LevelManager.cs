using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform StartPoint;
    public Transform[] path;

    public int currency;
    public int currentLevel = 1; // Dodanie aktualnego poziomu gry

    private void Awake()
    {
        main = this;
        // Nie niszcz obiektu miêdzy scenami
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currency = 100; // Inicjalizacja waluty
        // Wyœwietlanie poziomu na pocz¹tku
        DisplayLevelOnStart();
    }

    private void DisplayLevelOnStart()
    {
        // ZnajdŸ obiekt LevelDisplay i zaktualizuj wyœwietlany poziom
        LevelDisplay levelDisplay = FindObjectOfType<LevelDisplay>();
        if (levelDisplay != null)
        {
            levelDisplay.UpdateLevelDisplay(currentLevel);
        }
    }

    // Funkcja do przejœcia do nastêpnego poziomu
    public void NextLevel()
    {
        currentLevel++; // Zwiêksz poziom
        currency = 100; // Resetowanie waluty lub zmiana w zale¿noœci od wymagañ

        Debug.Log("Przechodzimy do poziomu " + currentLevel);

        // Wyœwietlanie nowego poziomu
        DisplayLevelOnStart();
    }

    // Funkcja do zwiêkszenia waluty
    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    // Funkcja do wydania waluty
    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("Nie masz wystarczaj¹cej iloœci waluty.");
            return false;
        }
    }
}
