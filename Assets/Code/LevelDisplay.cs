using UnityEngine;
using TMPro;
using System.Collections;

public class LevelDisplay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI levelText; 

    private void Start()
    {
        UpdateLevelDisplay(LevelManager.main.currentLevel); 
        StartCoroutine(HideLevelTextAfterDelay(3f)); 
    }

   
    public void UpdateLevelDisplay(int level)
    {
        levelText.text = "Level " + level; 
    }

    
    private IEnumerator HideLevelTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        levelText.gameObject.SetActive(false); 
    }
}
