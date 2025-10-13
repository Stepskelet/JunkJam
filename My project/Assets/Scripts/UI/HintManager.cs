using UnityEngine;
using TMPro;

public class HintManager : MonoBehaviour
{
    [SerializeField] private TMP_Text hintKey;
    [SerializeField] public GameObject hintBox;
    void Start()
    {
        hintBox.SetActive(false);
    }

    public void ShowHint(string key)
    {
        hintBox.SetActive(true);
        hintKey.text = key;
    }
    public void HideHint()
    {
        hintBox.SetActive(false);
    }
}
