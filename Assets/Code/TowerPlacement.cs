using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject towerPrefab; // Prefab wie¿y
    [SerializeField] private GameObject buildPointHighlightPrefab; // Prefab podœwietlenia (kwadrat)
    [SerializeField] private LayerMask BuildPoints; // Warstwa dla BuildPoint
    [SerializeField] private int towerCost = 100; // Koszt wie¿y

    private GameObject currentTower; // Aktualnie budowana wie¿a
    private Camera cam;
    private List<GameObject> highlightedPoints = new List<GameObject>(); // Lista podœwietlonych punktów

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        // Jeœli aktualnie mamy wie¿ê do postawienia, obs³ugujemy jej budowê
        if (currentTower != null)
        {
            HandleTowerPlacement();
        }
    }

    // Sprawdzenie, czy punkt jest dostêpny (czy nie ma ju¿ wie¿y)
    public bool IsAvailable(BuildPoint point)
    {
        return point.tower == null; // Jeœli `tower` w `BuildPoint` jest null, miejsce jest dostêpne
    }

    // Metoda do podœwietlania dostêpnych punktów budowy
    public void HighlightBuildPoints()
    {
        // Usuñ poprzednie podœwietlenia
        ClearHighlightedPoints();

        // ZnajdŸ wszystkie BuildPoint w scenie
        BuildPoint[] buildPoints = FindObjectsOfType<BuildPoint>();

        foreach (var point in buildPoints)
        {
            // SprawdŸ, czy punkt jest dostêpny
            if (IsAvailable(point))
            {
                // Tworzenie kwadratu podœwietlenia
                GameObject highlight = Instantiate(buildPointHighlightPrefab, point.transform.position, Quaternion.identity);
                highlight.GetComponent<SpriteRenderer>().color = Color.green; // Kolor na zielony

                highlightedPoints.Add(highlight); // Dodaj podœwietlenie do listy
            }
        }
    }

    // Metoda usuwaj¹ca podœwietlenia
    public void ClearHighlightedPoints()
    {
        foreach (var highlight in highlightedPoints)
        {
            Destroy(highlight); // Zniszcz instancje podœwietlenia
        }
        highlightedPoints.Clear(); // Wyczyœæ listê
    }

    // Rozpoczêcie budowy wie¿y
    public void StartPlacingTower()
    {
        // SprawdŸ, czy mamy wystarczaj¹c¹ iloœæ waluty
        if (LevelManager.main.currency >= towerCost)
        {
            currentTower = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity);
            currentTower.SetActive(false); // Ustaw wie¿ê jako nieaktywn¹
        }
        else
        {
            Debug.Log("Nie masz wystarczaj¹cej iloœci waluty!");
        }
    }

    // Obs³uguje umieszczanie wie¿y
    private void HandleTowerPlacement()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // Pozycja kursora myszy
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, BuildPoints); // Sprawdzenie, gdzie klikamy

        if (hit.collider != null)
        {
            // Ustawienie wie¿y w odpowiedniej pozycji
            currentTower.transform.position = hit.point;
            currentTower.SetActive(true); // Aktywuj wie¿ê, aby j¹ widzieæ

            // Jeœli kliknêliœmy lewym przyciskiem myszy, postaw wie¿ê
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower(hit); // Umieœæ wie¿ê w odpowiednim miejscu
            }
        }
        else
        {
            // Jeœli kursor nie wskazuje na dostêpny punkt, ukryj wie¿ê
            if (currentTower != null)
            {
                currentTower.SetActive(false);
            }
        }
    }

    // Umieszczenie wie¿y na wybranym punkcie
    public void PlaceTower(RaycastHit2D hit)
    {
        // Jeœli mamy wystarczaj¹c¹ iloœæ waluty, postaw wie¿ê
        if (LevelManager.main.SpendCurrency(towerCost))
        {
            // Pobranie pozycji œrodka BuildPoint
            Vector3 position = hit.collider.transform.position;

            // Tworzenie wie¿y w tej pozycji
            GameObject newTower = Instantiate(towerPrefab, position, Quaternion.identity);
            Debug.Log("Wie¿a postawiona na pozycji: " + position);

            // Przypisz wie¿ê do BuildPoint
            BuildPoint buildPoint = hit.collider.GetComponent<BuildPoint>();
            if (buildPoint != null)
            {
                buildPoint.tower = newTower; // Przypisz wie¿ê do tego punktu
            }
        }
        else
        {
            Debug.Log("Nie masz wystarczaj¹cej iloœci waluty!");
        }

        // Resetowanie aktualnie budowanej wie¿y
        currentTower = null;
    }
}
