using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerPlacement towerPlacementScript; // Script do budowania wie�y
    public GameObject tower; // Zmienna dla wie�y w tym punkcie, zmieni�em na publiczn�, aby mog�a by� dost�pna w innych skryptach

    // Funkcja do sprawdzania, czy miejsce jest dost�pne
    public bool IsAvailable()
    {
        return tower == null; // Je�li `tower` jest null, punkt jest dost�pny
    }

    private void OnMouseDown()
    {
        // Je�li na tym punkcie jest wie�a, pozw�l na jej interakcj� (np. przenoszenie)
        if (tower != null)
        {
            Debug.Log("Tego punktu nie mo�esz ju� zmieni�, poniewa� wie�a ju� tam stoi.");
        }
        else
        {
            // Je�li wie�a nie stoi, rozpocznij budow� wie�y
            if (towerPlacementScript != null)
            {
                towerPlacementScript.StartPlacingTower(); // Rozpocznij budow�
            }
            else
            {
                Debug.LogError("TowerPlacementScript is not assigned!");
            }
        }
    }

    // Mo�esz pozostawi� to, ale lepiej u�ywa� OnMouseDown do wykrywania klikni��
    private void OnMouseOver()
    {
        // Metoda, kt�ra mo�e pom�c w interakcjach wizualnych, ale nie jest konieczna do budowy
        if (Input.GetMouseButtonDown(0) && towerPlacementScript != null)
        {
            Debug.Log("MouseOver detected, clicking...");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                towerPlacementScript.PlaceTower(hit); // Umie�� wie��
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Wizualizacja kwadratu w edytorze (u�atwia debugowanie w Unity)
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 0)); // Kwadrat w 2D
    }
}
