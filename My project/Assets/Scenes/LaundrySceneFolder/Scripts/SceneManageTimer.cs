using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


public class SceneManageTimer : MonoBehaviour
{
    [Header("Время ожидания (секунды)")]
    public float waitTime = 60f;

    [Header("Image для затемнения экрана")]
    public Image fadeImage;

    [Header("Скорость затемнения")]
    public float fadeSpeed = 10f;

    [Header("Объект блина")]

    public GameObject pancake;

    [Header("Тег стиралок")]
    public String washerTag = "GameButton0";

    private bool isFading = false;
    private float fadeAlpha = 0f;

    private int washerosOff = 0;

    GameObject[] washers;

    private GameObject currentWasher;

    private List<int> availableNumbers = new List<int>();

    void Start()
    {
        washers = GameObject.FindGameObjectsWithTag(washerTag);

        foreach (GameObject w in washers)
        {
            w.GetComponent<StartGame>().enabled = false;
        }

        availableNumbers.Clear();
        for (int i = 0; i <= (washers.Length - 1); i++)
        {
            availableNumbers.Add(i);
        }

        currentWasher = washers[(int)GetUniqueRandomInt()];
        currentWasher.GetComponent<StartGame>().enabled = true;        

        pancake.SetActive(false);
        StartCoroutine(WaitAndFade());
    }

    void Update()
    {
        if (!currentWasher.GetComponent<StartGame>().enabled)
        {
            incCounter();

            if (washerosOff != 5)
            {
                currentWasher = washers[(int)GetUniqueRandomInt()];
                currentWasher.GetComponent<StartGame>().enabled = true;
            }  

        }
    }

    IEnumerator WaitAndFade()
    {
        yield return new WaitForSeconds(waitTime);

        if (washerosOff != 5)
        {
            isFading = true;
            while (fadeAlpha < 1f)
            {
                fadeAlpha += Time.deltaTime * fadeSpeed;
                fadeImage.color = new Color(0, 0, 0, fadeAlpha);
                yield return null;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void incCounter()
    {
        washerosOff++;

        if (washerosOff == 5 && !isFading)
        {
            pancake.SetActive(true);
            this.enabled = false;
        }
    }

    public int? GetUniqueRandomInt()
    {
        if (availableNumbers.Count == 0)
        {
            return null;
        }

        int index = UnityEngine.Random.Range(0, availableNumbers.Count);
        int number = availableNumbers[index];
        availableNumbers.RemoveAt(index);
        return number;
    }
}
