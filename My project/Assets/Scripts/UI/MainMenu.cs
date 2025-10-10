using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public static bool mainMenuActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenu.SetActive(true);
        mainMenuActive = true;
        settingsMenu.SetActive(false);
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
}
