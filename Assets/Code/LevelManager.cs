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
        // Nie niszcz obiektu mi�dzy scenami
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currency = 100; // Inicjalizacja waluty
        // Wy�wietlanie poziomu na pocz�tku
        DisplayLevelOnStart();
    }

    private void DisplayLevelOnStart()
    {
        // Znajd� obiekt LevelDisplay i zaktualizuj wy�wietlany poziom
        LevelDisplay levelDisplay = FindObjectOfType<LevelDisplay>();
        if (levelDisplay != null)
        {
            levelDisplay.UpdateLevelDisplay(currentLevel);
        }
    }

    // Funkcja do przej�cia do nast�pnego poziomu
    public void NextLevel()
    {
        currentLevel++; // Zwi�ksz poziom
        currency = 100; // Resetowanie waluty lub zmiana w zale�no�ci od wymaga�

        Debug.Log("Przechodzimy do poziomu " + currentLevel);

        // Wy�wietlanie nowego poziomu
        DisplayLevelOnStart();
    }

    // Funkcja do zwi�kszenia waluty
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
            Debug.Log("Nie masz wystarczaj�cej ilo�ci waluty.");
            return false;
        }
    }
}
