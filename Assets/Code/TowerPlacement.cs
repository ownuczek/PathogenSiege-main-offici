using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject towerPrefab; // Prefab wie¿y do postawienia
    [SerializeField] private LayerMask placementLayer; // Warstwa, w której mo¿na postawiæ wie¿ê (np. tylko w okreœlonych miejscach)
    [SerializeField] private int towerCost = 100; // Koszt postawienia wie¿y

    private GameObject currentTower; // Obiekt tymczasowy, który pokazuje, gdzie bêdzie postawiona wie¿a
    private Camera cam;
    private bool isPlacing = false;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (isPlacing)
        {
            HandleTowerPlacement();
        }
    }

    // Funkcja uruchamiaj¹ca tryb stawiania wie¿y
    public void StartPlacingTower()
    {
        
        {
            if (LevelManager.main.currency >= towerCost)
            {
                isPlacing = true;
                currentTower = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity);
                Debug.Log("Tower instantiated: " + currentTower.name);
                currentTower.SetActive(false);
            }
            else
            {
                Debug.Log("Not enough currency to place tower.");
            }
        }

        if (LevelManager.main.currency >= towerCost) // SprawdŸ, czy gracz ma wystarczaj¹c¹ iloœæ waluty
        {
            isPlacing = true;
            currentTower = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity); // Utwórz wie¿ê na ekranie
            currentTower.SetActive(false); // Na pocz¹tku nie pokazuj wie¿y
            Debug.Log("Rozpoczêto stawianie wie¿y.");
        }
        else
        {
            Debug.Log("Nie masz wystarczaj¹cej iloœci waluty, aby rozpocz¹æ stawianie wie¿y.");
        }
    }

    // Funkcja do wykrywania miejsca, gdzie ma zostaæ postawiona wie¿a
    private void HandleTowerPlacement()
    {
        
        {
            Debug.Log("HandleTowerPlacement is running!");
            // Reszta kodu...
        }


        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); // Pozycja myszy na ekranie
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, placementLayer);

        if (hit.collider != null)
        {
            // Ustaw pozycjê wie¿y zgodnie z miejscem, w którym gracz wskazuje
            currentTower.transform.position = hit.point;
            currentTower.SetActive(true); // Poka¿ wie¿ê

            Debug.Log("Wie¿a jest na poprawnym miejscu: " + hit.point);

            // Zmieñ kolor wie¿y na zielony
            var renderer = currentTower.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = Color.green;
            }

            // Jeœli gracz kliknie, postaw wie¿ê
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTowerOnGround(hit);
            }
        }
        else
        {
            // Jeœli wie¿a jest w niepoprawnym miejscu
            Debug.Log("Wie¿a jest w niepoprawnym miejscu.");

            if (currentTower != null)
            {
                currentTower.SetActive(true);
                var renderer = currentTower.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    renderer.color = Color.red; // Kolor czerwony dla niepoprawnego miejsca
                }
            }
        }
    }

    // Funkcja, która umieszcza wie¿ê na mapie
    private void PlaceTowerOnGround(RaycastHit2D hit)
    {
       
        {
            Debug.Log("PlaceTowerOnGround called!");
            // Reszta kodu...
        }

        if (LevelManager.main.currency >= towerCost)
        {
            LevelManager.main.currency -= towerCost; // Zmniejsz walutê gracza
            Debug.Log("Wie¿a zosta³a postawiona na miejscu: " + hit.point);

            currentTower.SetActive(true); // Ostatecznie poka¿ wie¿ê
            isPlacing = false; // Zakoñcz tryb stawiania wie¿y
            currentTower = null; // Zresetuj obiekt
        }
        else
        {
            Debug.Log("Nie masz wystarczaj¹cej iloœci waluty!");
            Destroy(currentTower); // Zniszcz obiekt, jeœli nie staæ na niego
            currentTower = null; // Zresetuj obiekt
            isPlacing = false; // Zakoñcz tryb stawiania wie¿y
        }

    }
}
