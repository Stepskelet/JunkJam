using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        mainMenu.SetActive(false);
    }
}
