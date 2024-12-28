using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform StartPoint;
    public Transform[] path;

    public int currency;
    public int currentLevel = 1;

    private void Awake()
    {
        main = this;
       
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currency = 100; 
       
        DisplayLevelOnStart();
    }

    private void DisplayLevelOnStart()
    {
        
        LevelDisplay levelDisplay = FindObjectOfType<LevelDisplay>();
        if (levelDisplay != null)
        {
            levelDisplay.UpdateLevelDisplay(currentLevel);
        }
    }

   
    public void NextLevel()
    {
        currentLevel++; 
        currency = 100;

        Debug.Log("Przechodzimy do poziomu " + currentLevel);

        
        DisplayLevelOnStart();
    }

    
    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

   
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
