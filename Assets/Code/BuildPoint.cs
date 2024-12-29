using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TowerPlacement towerPlacementScript; 

    private void OnMouseDown()
    {
        
        Debug.Log("BuildPoint clicked");

        if (towerPlacementScript != null)
        {
            towerPlacementScript.StartPlacingTower(); 
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
                towerPlacementScript.PlaceTower(hit);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 0)); // Rozmiar kwadratu siatki
    }

}
