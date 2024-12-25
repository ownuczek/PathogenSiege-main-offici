using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float damageInterval = 1f;
    [SerializeField] private int damagePerVirus = 10;

    private int currentHealth;
    private List<EnemyMovement> attackingViruses = new List<EnemyMovement>();
    private GameOverManager gameOverManager;
    private Coroutine damageCoroutine; // Do przechowywania referencji do coroutiny

    private void Start()
    {
        currentHealth = maxHealth;
        gameOverManager = FindObjectOfType<GameOverManager>();

        if (gameOverManager == null)
        {
            Debug.LogError("GameOverManager not found in the scene!");
        }
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            DestroyNexus();
        }
    }

    public void StartVirusAttack(EnemyMovement virus)
    {
        attackingViruses.Add(virus);

        if (attackingViruses.Count == 1 && damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(HandleDamageOverTime());
        }
    }

    // Metoda do zakoñczenia ataku wirusa
    public void StopVirusAttack(EnemyMovement virus)
    {
        attackingViruses.Remove(virus);

        if (attackingViruses.Count == 0 && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    // Coroutine do obs³ugi zadawania obra¿eñ przez wirusy
    private IEnumerator HandleDamageOverTime()
    {
        while (attackingViruses.Count > 0 && currentHealth > 0)
        {
            currentHealth -= damagePerVirus * attackingViruses.Count; // Obra¿enia od wszystkich wirusów
            Debug.Log($"Nexus HP: {currentHealth}");
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void DestroyNexus()
    {
        Debug.Log("Nexus zosta³ zniszczony! Koniec gry.");

        // Wywo³ujemy ekran Game Over
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver(); // Wyœwietlamy ekran Game Over
        }
    }
}
