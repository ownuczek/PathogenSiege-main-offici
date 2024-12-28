using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private LayerMask BuildPoints; 
    [SerializeField] private int towerCost = 100; 

    private GameObject currentTower; 
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        
        if (currentTower != null)
        {
            HandleTowerPlacement();
        }
    }

    
    public void StartPlacingTower()
    {
        
        if (LevelManager.main.currency >= towerCost)
        {
            currentTower = Instantiate(towerPrefab, Vector3.zero, Quaternion.identity);
            currentTower.SetActive(false); 
        }
        else
        {
            Debug.Log("Nie masz wystarczaj¹cej iloœci waluty!");
        }
    }

    
    private void HandleTowerPlacement()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); 
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, BuildPoints);

        if (hit.collider != null)
        {
            
            currentTower.transform.position = hit.point;
            currentTower.SetActive(true); // Poka¿ wie¿ê

            
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower(hit); 
            }
        }
        else
        {
            
            if (currentTower != null)
            {
                currentTower.SetActive(false);
            }
        }
    }

    
    public void PlaceTower(RaycastHit2D hit)
    {
        
        if (LevelManager.main.SpendCurrency(towerCost))
        {
            Instantiate(towerPrefab, hit.point, Quaternion.identity); 
            Debug.Log("Wie¿a postawiona na pozycji: " + hit.point);
        }
        else
        {
            Debug.Log("Nie masz wystarczaj¹cej iloœci waluty!");
        }

        
        currentTower = null;
    }
}
