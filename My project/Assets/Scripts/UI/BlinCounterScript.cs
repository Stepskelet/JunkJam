using UnityEngine;
using TMPro;

public class BlinCounterScript : MonoBehaviour
{
    [SerializeField] private TMP_Text counterText;
    [SerializeField] public GameObject counterObject;

    private int blinCount = 0;

    private void Start()
    {
        counterObject.SetActive(true);
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
