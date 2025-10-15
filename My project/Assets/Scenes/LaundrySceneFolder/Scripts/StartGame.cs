using NUnit.Framework;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public float shakeAmount = 0.05f;      // Амплитуда тряски (насколько сильно трясёт)
    public float shakeFrequency = 10f;     // Частота тряски (скорость колебаний)
    public bool shakeRotation = true;      // Трясти ли вращением

    public Game miniGame;

    public float interactDistance = 1f;    // Дистанция взаимодействия

    public string soundTag = "WashingAudio";

    private GameObject sourceAudio;

    private AudioSource toPlay;

    private float soundStart = 6f;

    private float soundEnd = 30f;

    private bool isPlaying;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void OnEnable()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
        miniGame.enabled = false;

        sourceAudio = GameObject.FindWithTag(soundTag);
        sourceAudio.transform.position = transform.position;
        toPlay = sourceAudio.GetComponent<AudioSource>();
        toPlay.time = soundStart;
        toPlay.Play();
    }

    void OnDisable()
    {
        isPlaying = false;
        transform.localPosition = initialPosition;
        transform.localRotation = initialRotation;
        toPlay.Stop();
    }

    void Update()
    {
        if (!miniGame.enabled && isPlaying)
        {
            enabled = false;
        }

        if (toPlay.isPlaying)
        {
            if (toPlay.time >= soundEnd)
            {
                toPlay.time = soundStart;
            }
        }

        float shakeX = Mathf.Sin(Time.time * shakeFrequency) * shakeAmount;
        float shakeY = Mathf.Cos(Time.time * shakeFrequency * 1.3f) * shakeAmount;

        transform.localPosition = initialPosition + new Vector3(shakeX, shakeY, 0);

        if (shakeRotation)
        {
            float rotZ = Mathf.Sin(Time.time * shakeFrequency) * shakeAmount * 10f;
            transform.localRotation = initialRotation * Quaternion.Euler(0, 0, rotZ);
        }

        
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        if (Input.GetKeyDown(KeyCode.E) && !isPlaying)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    isPlaying = true;
                    miniGame.enabled = true;
                }   
            }
        }

        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     MoveObjectInFrontOfCamera();
        // }
    }
}
