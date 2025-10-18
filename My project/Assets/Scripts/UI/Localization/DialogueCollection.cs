using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class DialogueCollection : MonoBehaviour
{
    private int sceneIndex;
    public List<string> lines;
    private int locale;
    public List<float> timing;

    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        locale = LocaleSelector.localeID;
    }

    private void FixedUpdate()
    {
        locale = LocaleSelector.localeID;
        ChooseLines();
    }

    public void ChooseLines()
    {
        switch ((sceneIndex, locale))
        {
            case (0, 0): // start scene
                lines = new List<string> { "Where...", "...Am I?..", "Why am I so hugry?.." };
                timing = new List<float> {3, 4, 3};
                break;
            case (0, 1):
                lines = new List<string> { "Где...", "...Где я...", "Так хочется есть..." };
                timing = new List<float> { 3, 4, 3 };
                break;

            case (1, 0): // kitchen
                lines = new List<string> { "This was actually pretty good!", "*coughing*", "I need to turn *coughing* off damn stoves (Use 'E' key)", "*coughing*", "It brings me back to the time, when I burned down the place, I used to work in." };
                timing = new List<float> { 2, 2, 4, 2, 6 };
                break;
            case (1, 1):
                lines = new List<string> { "Неплохой был блинчик.", "*кашель*","Надо вы-*кашель*-ключить плиты (Используйте клавишу 'Е')" , "*кашель*", "Навевает воспоминания о том, как я устрои пождог общепита, в котором работал."};
                timing = new List<float> { 2, 2, 4, 2, 5 };
                break;

            case (2, 0): // laundry
                lines = new List<string> { "It doesn't get any better.", "Litterally... I guess...", "(Use 'E' key to interact with washers)"};
                timing = new List<float> { 3, 3, 3};
                break;
            case (2, 1):
                lines = new List<string> { "Лучше и быть не могло.", "Видимо, буквально...", "Используйте клавишу 'Е' для взаимодействия со стиральными машинами"};
                timing = new List<float> { 3, 3, 3};
                break;

            case (3, 0): // end scenes
                lines = new List<string> { "I miss pancakes, my mom used to make...", "I should try to cook me some" };
                timing = new List<float> { 4, 4 };
                break;
            case (3, 1):
                lines = new List<string> { "Я скучаю по блинам, которые делала мама...", "Надо попробовать сделать их самому" };
                timing = new List<float> { 4, 4 };
                break;

            default:
                lines = null;
                timing = null;
                break;
        }
    }
}
