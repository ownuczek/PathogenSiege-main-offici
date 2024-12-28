using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject towerPrefab; // Prefab wie�y
    [SerializeField] private LayerMask BuildPoints; // Warstwa punkt�w budowy
    [SerializeField] private int towerCost = 100; // Koszt stawiania wie�y

    private GameObject currentTower; // Obiekt wie�y, kt�r� gracz chce postawi�
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        // Sprawd�, czy gracz stawia wie��
        if (currentTower != null)
        {
            HandleTowerPlacement();
        }
    }

    // Rozpoczynanie procesu stawiania wie�y
    public void StartPlacingTower()
    {
        // Sprawdzenie, czy gracz ma wystarczaj�c� ilo�� waluty
        if (LevelManager.main.currency >= towerCost)
        {
            currentTower = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity); // Utw�rz wie�� w tym samym miejscu co kursor
            currentTower.SetActive(false); // Na pocz�tku nie pokazujemy wie�y
        }
        else
        {
            Debug.Log("Nie masz wystarczaj�cej ilo�ci waluty!");
        }
    }

    // Funkcja do wykrywania miejsca, w kt�rym ma zosta� postawiona wie�a
    private void HandleTowerPlacement()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // Pozycja myszy na ekranie
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, BuildPoints); // Raycast w kierunku punktu budowy

        if (hit.collider != null)
        {
            // Je�li trafi�o w punkt budowy, pokazujemy, gdzie postawimy wie��
            currentTower.transform.position = hit.point;
            currentTower.SetActive(true); // Poka� wie��

            // Je�li gracz kliknie, stawiamy wie��
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower(hit); // U�ywamy nowej wersji tej funkcji
            }
        }
        else
        {
            // Je�li nie trafiono w punkt budowy, wie�a nie jest aktywna
            if (currentTower != null)
            {
                currentTower.SetActive(false);
            }
        }
    }

    // Funkcja, kt�ra umieszcza wie�� na mapie
    public void PlaceTower(RaycastHit2D hit) // Zmieniamy modyfikator dost�pu na publiczny
    {
        // Je�li gracz ma wystarczaj�c� ilo�� waluty
        if (LevelManager.main.SpendCurrency(towerCost))
        {
            Instantiate(towerPrefab, hit.point, Quaternion.identity); // Stw�rz wie�� w miejscu klikni�cia
            Debug.Log("Wie�a postawiona na pozycji: " + hit.point);
        }
        else
        {
            Debug.Log("Nie masz wystarczaj�cej ilo�ci waluty!");
        }

        // Zresetuj stan po postawieniu wie�y
        currentTower = null;
    }
}
