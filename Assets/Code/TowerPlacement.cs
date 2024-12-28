using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject towerPrefab; // Prefab wie¿y
    [SerializeField] private LayerMask BuildPoints; // Warstwa punktów budowy
    [SerializeField] private int towerCost = 100; // Koszt stawiania wie¿y

    private GameObject currentTower; // Obiekt wie¿y, któr¹ gracz chce postawiæ
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        // SprawdŸ, czy gracz stawia wie¿ê
        if (currentTower != null)
        {
            HandleTowerPlacement();
        }
    }

    // Rozpoczynanie procesu stawiania wie¿y
    public void StartPlacingTower()
    {
        // Sprawdzenie, czy gracz ma wystarczaj¹c¹ iloœæ waluty
        if (LevelManager.main.currency >= towerCost)
        {
            currentTower = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity); // Utwórz wie¿ê w tym samym miejscu co kursor
            currentTower.SetActive(false); // Na pocz¹tku nie pokazujemy wie¿y
        }
        else
        {
            Debug.Log("Nie masz wystarczaj¹cej iloœci waluty!");
        }
    }

    // Funkcja do wykrywania miejsca, w którym ma zostaæ postawiona wie¿a
    private void HandleTowerPlacement()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // Pozycja myszy na ekranie
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, BuildPoints); // Raycast w kierunku punktu budowy

        if (hit.collider != null)
        {
            // Jeœli trafi³o w punkt budowy, pokazujemy, gdzie postawimy wie¿ê
            currentTower.transform.position = hit.point;
            currentTower.SetActive(true); // Poka¿ wie¿ê

            // Jeœli gracz kliknie, stawiamy wie¿ê
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower(hit); // U¿ywamy nowej wersji tej funkcji
            }
        }
        else
        {
            // Jeœli nie trafiono w punkt budowy, wie¿a nie jest aktywna
            if (currentTower != null)
            {
                currentTower.SetActive(false);
            }
        }
    }

    // Funkcja, która umieszcza wie¿ê na mapie
    public void PlaceTower(RaycastHit2D hit) // Zmieniamy modyfikator dostêpu na publiczny
    {
        // Jeœli gracz ma wystarczaj¹c¹ iloœæ waluty
        if (LevelManager.main.SpendCurrency(towerCost))
        {
            Instantiate(towerPrefab, hit.point, Quaternion.identity); // Stwórz wie¿ê w miejscu klikniêcia
            Debug.Log("Wie¿a postawiona na pozycji: " + hit.point);
        }
        else
        {
            Debug.Log("Nie masz wystarczaj¹cej iloœci waluty!");
        }

        // Zresetuj stan po postawieniu wie¿y
        currentTower = null;
    }
}
