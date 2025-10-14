using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject credits;
    public static bool mainMenuActive = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            mainMenu.SetActive(true);
            mainMenuActive = true;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            mainMenu.SetActive(false);
            mainMenuActive = false;
        }

        settingsMenu.SetActive(false);
        credits.SetActive(false);

    }

    private void Update()
    {
        if (credits.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCredits();
        }
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        mainMenuActive = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void OpenSettingsMenu()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void OpenCredits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }
    public void CloseCredits()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }
}
