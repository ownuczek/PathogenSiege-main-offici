using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private EnemySpawner enemySpawner;  // Dodajemy referencjê do EnemySpawner

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f; // Prêdkoœæ poruszania siê wroga

    private Transform target;
    private int pathIndex = 0;
    private bool attackingNexus = false; // Czy wirus atakuje Nexusa?

    private void Start()
    {
        target = LevelManager.main.path[pathIndex];
    }

    private void Update()
    {
        if (target == null)
        {
            Debug.LogError("Target jest null! Œcie¿ka nie zosta³a poprawnie ustawiona.");
            return;
        }

        if (attackingNexus) return; // Wirus nie porusza siê, gdy atakuje Nexus

        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                // Dotarcie do Nexusa
                StartAttackingNexus();
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        if (attackingNexus) return; // Zatrzymaj ruch, jeœli wirus atakuje Nexus

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed; // Zastosowanie zmiennej moveSpeed
    }

    private void StartAttackingNexus()
    {
        attackingNexus = true;
        rb.velocity = Vector2.zero; // Zatrzymaj ruch

        // ZnajdŸ Nexus i zacznij go atakowaæ
        Nexus nexus = FindObjectOfType<Nexus>();
        if (nexus != null)
        {
            nexus.StartVirusAttack(this);
        }
        else
        {
            Debug.LogError("Nexus nie znaleziony!");
            Destroy(gameObject); // Usuñ wirusa, jeœli Nexus nie istnieje
        }
    }

    public void StopAttacking()
    {
        // Wywo³ywane przez Nexus, gdy wirus przestaje atakowaæ (np. ginie)
        Destroy(gameObject);
    }

    // Dodajemy metodê, aby zmieniaæ prêdkoœæ wroga z zewn¹trz
    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
        Debug.Log($"Prêdkoœæ ustawiona na: {moveSpeed}");
    }

    // Teraz wywo³anie EndWave w zale¿noœci od stanu wroga
    public void EndWave()
    {
        if (enemySpawner != null)
        {
            enemySpawner.EndWave(); // Wywo³anie metody EndWave z EnemySpawner
        }
    }
}
