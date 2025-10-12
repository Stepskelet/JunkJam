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
        mainMenu.SetActive(true);
        mainMenuActive = true;
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
