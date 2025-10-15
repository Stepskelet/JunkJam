using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BlinCounterScript : MonoBehaviour
{
    [SerializeField] private TMP_Text counterText;
    [SerializeField] public GameObject counterObject;

    private static int blinCount = 0;

    private void Start()
    {
        counterObject.SetActive(true);
        counterText.text = blinCount.ToString();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            counterText.text = "0";
            blinCount = 0;
        }
    }

    private void FixedUpdate()
    {
        if(MainMenu.mainMenuActive||PauseMenu.isPaused)
        {
            counterObject.SetActive(false);
        }
        else
        {
            counterObject.SetActive(true);
        }
    }

    public void UpdateCounter(int amount)
    {
        blinCount += amount;
        counterText.text = blinCount.ToString();
    }
}
