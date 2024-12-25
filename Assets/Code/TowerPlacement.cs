using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject towerPrefab; // Prefab wie�y do postawienia
    [SerializeField] private LayerMask placementLayer; // Warstwa, w kt�rej mo�na postawi� wie�� (np. tylko w okre�lonych miejscach)
    [SerializeField] private int towerCost = 100; // Koszt postawienia wie�y

    private GameObject currentTower; // Obiekt tymczasowy, kt�ry pokazuje, gdzie b�dzie postawiona wie�a
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

    // Funkcja uruchamiaj�ca tryb stawiania wie�y
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

        if (LevelManager.main.currency >= towerCost) // Sprawd�, czy gracz ma wystarczaj�c� ilo�� waluty
        {
            isPlacing = true;
            currentTower = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity); // Utw�rz wie�� na ekranie
            currentTower.SetActive(false); // Na pocz�tku nie pokazuj wie�y
            Debug.Log("Rozpocz�to stawianie wie�y.");
        }
        else
        {
            Debug.Log("Nie masz wystarczaj�cej ilo�ci waluty, aby rozpocz�� stawianie wie�y.");
        }
    }

    // Funkcja do wykrywania miejsca, gdzie ma zosta� postawiona wie�a
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
            // Ustaw pozycj� wie�y zgodnie z miejscem, w kt�rym gracz wskazuje
            currentTower.transform.position = hit.point;
            currentTower.SetActive(true); // Poka� wie��

            Debug.Log("Wie�a jest na poprawnym miejscu: " + hit.point);

            // Zmie� kolor wie�y na zielony
            var renderer = currentTower.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = Color.green;
            }

            // Je�li gracz kliknie, postaw wie��
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTowerOnGround(hit);
            }
        }
        else
        {
            // Je�li wie�a jest w niepoprawnym miejscu
            Debug.Log("Wie�a jest w niepoprawnym miejscu.");

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

    // Funkcja, kt�ra umieszcza wie�� na mapie
    private void PlaceTowerOnGround(RaycastHit2D hit)
    {
       
        {
            Debug.Log("PlaceTowerOnGround called!");
            // Reszta kodu...
        }

        if (LevelManager.main.currency >= towerCost)
        {
            LevelManager.main.currency -= towerCost; // Zmniejsz walut� gracza
            Debug.Log("Wie�a zosta�a postawiona na miejscu: " + hit.point);

            currentTower.SetActive(true); // Ostatecznie poka� wie��
            isPlacing = false; // Zako�cz tryb stawiania wie�y
            currentTower = null; // Zresetuj obiekt
        }
        else
        {
            Debug.Log("Nie masz wystarczaj�cej ilo�ci waluty!");
            Destroy(currentTower); // Zniszcz obiekt, je�li nie sta� na niego
            currentTower = null; // Zresetuj obiekt
            isPlacing = false; // Zako�cz tryb stawiania wie�y
        }

    }
}
