using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject tintPanel;
    public static bool isPaused = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    private void Update()
    {
        if (!MainMenu.mainMenuActive)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused) PauseGame();
                else ResumeGame();
            }
        }
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void GoToMainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenSettingsMenu()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    
}
