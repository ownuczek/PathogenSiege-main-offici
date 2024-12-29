using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject towerPrefab; // Prefab wie�y
    [SerializeField] private GameObject buildPointHighlightPrefab; // Prefab pod�wietlenia (kwadrat)
    [SerializeField] private LayerMask BuildPoints; // Warstwa dla BuildPoint
    [SerializeField] private int towerCost = 100; // Koszt wie�y

    private GameObject currentTower; // Aktualnie budowana wie�a
    private Camera cam;
    private List<GameObject> highlightedPoints = new List<GameObject>(); // Lista pod�wietlonych punkt�w

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        // Je�li aktualnie mamy wie�� do postawienia, obs�ugujemy jej budow�
        if (currentTower != null)
        {
            HandleTowerPlacement();
        }
    }

    // Sprawdzenie, czy punkt jest dost�pny (czy nie ma ju� wie�y)
    public bool IsAvailable(BuildPoint point)
    {
        return point.tower == null; // Je�li `tower` w `BuildPoint` jest null, miejsce jest dost�pne
    }

    // Metoda do pod�wietlania dost�pnych punkt�w budowy
    public void HighlightBuildPoints()
    {
        // Usu� poprzednie pod�wietlenia
        ClearHighlightedPoints();

        // Znajd� wszystkie BuildPoint w scenie
        BuildPoint[] buildPoints = FindObjectsOfType<BuildPoint>();

        foreach (var point in buildPoints)
        {
            // Sprawd�, czy punkt jest dost�pny
            if (IsAvailable(point))
            {
                // Tworzenie kwadratu pod�wietlenia
                GameObject highlight = Instantiate(buildPointHighlightPrefab, point.transform.position, Quaternion.identity);
                highlight.GetComponent<SpriteRenderer>().color = Color.green; // Kolor na zielony

                highlightedPoints.Add(highlight); // Dodaj pod�wietlenie do listy
            }
        }
    }

    // Metoda usuwaj�ca pod�wietlenia
    public void ClearHighlightedPoints()
    {
        foreach (var highlight in highlightedPoints)
        {
            Destroy(highlight); // Zniszcz instancje pod�wietlenia
        }
        highlightedPoints.Clear(); // Wyczy�� list�
    }

    // Rozpocz�cie budowy wie�y
    public void StartPlacingTower()
    {
        // Sprawd�, czy mamy wystarczaj�c� ilo�� waluty
        if (LevelManager.main.currency >= towerCost)
        {
            currentTower = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity);
            currentTower.SetActive(false); // Ustaw wie�� jako nieaktywn�
        }
        else
        {
            Debug.Log("Nie masz wystarczaj�cej ilo�ci waluty!");
        }
    }

    // Obs�uguje umieszczanie wie�y
    private void HandleTowerPlacement()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // Pozycja kursora myszy
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, BuildPoints); // Sprawdzenie, gdzie klikamy

        if (hit.collider != null)
        {
            // Ustawienie wie�y w odpowiedniej pozycji
            currentTower.transform.position = hit.point;
            currentTower.SetActive(true); // Aktywuj wie��, aby j� widzie�

            // Je�li klikn�li�my lewym przyciskiem myszy, postaw wie��
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower(hit); // Umie�� wie�� w odpowiednim miejscu
            }
        }
        else
        {
            // Je�li kursor nie wskazuje na dost�pny punkt, ukryj wie��
            if (currentTower != null)
            {
                currentTower.SetActive(false);
            }
        }
    }

    // Umieszczenie wie�y na wybranym punkcie
    public void PlaceTower(RaycastHit2D hit)
    {
        // Je�li mamy wystarczaj�c� ilo�� waluty, postaw wie��
        if (LevelManager.main.SpendCurrency(towerCost))
        {
            // Pobranie pozycji �rodka BuildPoint
            Vector3 position = hit.collider.transform.position;

            // Tworzenie wie�y w tej pozycji
            GameObject newTower = Instantiate(towerPrefab, position, Quaternion.identity);
            Debug.Log("Wie�a postawiona na pozycji: " + position);

            // Przypisz wie�� do BuildPoint
            BuildPoint buildPoint = hit.collider.GetComponent<BuildPoint>();
            if (buildPoint != null)
            {
                buildPoint.tower = newTower; // Przypisz wie�� do tego punktu
            }
        }
        else
        {
            Debug.Log("Nie masz wystarczaj�cej ilo�ci waluty!");
        }

        // Resetowanie aktualnie budowanej wie�y
        currentTower = null;
    }
}
