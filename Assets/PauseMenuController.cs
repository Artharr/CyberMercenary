using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    private bool paused;
    void Start()
    {
        HidePauseMenu();
        paused = false;
        Debug.Log(paused);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                ShowPauseMenu(); 
            }
            else
            {
                HidePauseMenu(); 
            }

            
        }
    }


    private void ShowPauseMenu()
    {
       
        foreach (Transform child in transform) 
        {
            child.gameObject.SetActive(true); 
        }
        paused = !paused;
        Time.timeScale = 0;
        
    }

    public void HidePauseMenu()
    {
        foreach (Transform child in transform) 
        {
            child.gameObject.SetActive(false); 
        }
        paused = !paused;
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void BackToMenuLevels()
    {
        SceneManager.LoadScene("InGameMenu");
    }
}
