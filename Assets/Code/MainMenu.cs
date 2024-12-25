using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // Jeœli scena 'Level 1' jest na drugim miejscu w Build Settings (indeks 1)
        ;
    }

    
    public void QuitGame()
    {
        Application.Quit();
    }
}
