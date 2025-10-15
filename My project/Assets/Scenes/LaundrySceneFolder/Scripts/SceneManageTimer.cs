using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using NUnit.Framework;


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


    [Header("Канвас")]

    public Image waterImage;

    [Header("Частицы воды")]

    public GameObject particles;

    [Header("Объект затопления")]

    public GameObject flood;

    [Header("Аудио")]
    public string soundTag = "WaterAudio";

    public string winTag = "WinSound";

    public string underTag = "UnderSound";

    private float elapsedTime = 0f;
    private bool isMoving = false;

    private bool isMovingDown = false;

    private float startPosition;
    private float endPosition;

    private bool isFading = false;
    private float fadeAlpha = 0f;

    private int washerosOff = 0;

    private AudioSource waterAudio;

    private AudioSource winAudio;

    private AudioSource underAudio;


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

        GameObject sourceAudio = GameObject.FindGameObjectWithTag(soundTag);
        waterAudio = sourceAudio.GetComponent<AudioSource>();
        sourceAudio = GameObject.FindGameObjectWithTag(winTag);
        winAudio = sourceAudio.GetComponent<AudioSource>();
        sourceAudio = GameObject.FindGameObjectWithTag(underTag);
        underAudio = sourceAudio.GetComponent<AudioSource>();
        waterAudio.Play();

        currentWasher = washers[(int)GetUniqueRandomInt()];
        currentWasher.GetComponent<StartGame>().enabled = true;

        pancake.SetActive(false);

        elapsedTime = 0f;
        startPosition = flood.transform.position.y;
        endPosition = Camera.main.transform.position.y - 5.05f;
        isMoving = true;

        StartCoroutine(WaitAndFade());
    }

    void Update()
    {
        if (isMoving || isMovingDown)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / waitTime);
            float newValue = Mathf.Lerp(startPosition, endPosition, t);
            Vector3 pos = flood.transform.position;
            pos.y = newValue;
            flood.transform.position = pos;

            if (t >= 1f)
            {
                if (isMoving)
                {
                    isMoving = false;
                    waterImage.enabled = true;
                    particles.SetActive(true);
                    waterAudio.Stop();
                    underAudio.Play();
                }

                else
                {
                    flood.SetActive(false);
                    waterAudio.Stop();
                    this.enabled = false;
                    return;
                }
            }
        }

        if (!isMovingDown)
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
    }

    IEnumerator WaitAndFade()
    {
        float fadeWait = waitTime;
        yield return new WaitForSeconds(fadeWait);

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

        if (washerosOff == 5 && !isFading && isMoving)
        {
            pancake.SetActive(true);
            winAudio.Play();
            isMoving = false;
            isMovingDown = true;

            elapsedTime = 0f;
            waitTime = 3f;
            endPosition = startPosition;
            startPosition = flood.transform.position.y;
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
