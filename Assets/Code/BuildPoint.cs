using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerPlacement towerPlacementScript; // Odwo³anie do skryptu TowerPlacement

    private void OnMouseDown()
    {
        // Debugging info
        Debug.Log("BuildPoint clicked");

        if (towerPlacementScript != null)
        {
            towerPlacementScript.StartPlacingTower(); // Rozpoczynamy proces stawiania wie¿y
        }
        else
        {
            Debug.Log("TowerPlacementScript is not assigned!");
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && towerPlacementScript != null)
        {
            Debug.Log("MouseOver detected, clicking...");
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                towerPlacementScript.PlaceTower(hit); // Ustawiamy wie¿ê
            }
        }
    }
}
