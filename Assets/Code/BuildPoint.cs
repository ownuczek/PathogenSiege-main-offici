using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerPlacement towerPlacementScript; // Script do budowania wie¿y
    public GameObject tower; // Zmienna dla wie¿y w tym punkcie, zmieni³em na publiczn¹, aby mog³a byæ dostêpna w innych skryptach

    // Funkcja do sprawdzania, czy miejsce jest dostêpne
    public bool IsAvailable()
    {
        return tower == null; // Jeœli `tower` jest null, punkt jest dostêpny
    }

    private void OnMouseDown()
    {
        // Jeœli na tym punkcie jest wie¿a, pozwól na jej interakcjê (np. przenoszenie)
        if (tower != null)
        {
            Debug.Log("Tego punktu nie mo¿esz ju¿ zmieniæ, poniewa¿ wie¿a ju¿ tam stoi.");
        }
        else
        {
            // Jeœli wie¿a nie stoi, rozpocznij budowê wie¿y
            if (towerPlacementScript != null)
            {
                towerPlacementScript.StartPlacingTower(); // Rozpocznij budowê
            }
            else
            {
                Debug.LogError("TowerPlacementScript is not assigned!");
            }
        }
    }

    // Mo¿esz pozostawiæ to, ale lepiej u¿ywaæ OnMouseDown do wykrywania klikniêæ
    private void OnMouseOver()
    {
        // Metoda, która mo¿e pomóc w interakcjach wizualnych, ale nie jest konieczna do budowy
        if (Input.GetMouseButtonDown(0) && towerPlacementScript != null)
        {
            Debug.Log("MouseOver detected, clicking...");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                towerPlacementScript.PlaceTower(hit); // Umieœæ wie¿ê
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Wizualizacja kwadratu w edytorze (u³atwia debugowanie w Unity)
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 0)); // Kwadrat w 2D
    }
}
